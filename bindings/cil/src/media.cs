/**
 * @file media.cs
 * @brief Media descriptor class
 * @ingroup API
 */

/**********************************************************************
 *  Copyright (C) 2007-2009 Rémi Denis-Courmont.                      *
 *  This program is free software; you can redistribute and/or modify *
 *  it under the terms of the GNU General Public License as published *
 *  by the Free Software Foundation; version 2 of the license, or (at *
 *  your option) any later version.                                   *
 *                                                                    *
 *  This program is distributed in the hope that it will be useful,   *
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of    *
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.              *
 *  See the GNU General Public License for more details.              *
 *                                                                    *
 *  You should have received a copy of the GNU General Public License *
 *  along with this program; if not, you can get it from:             *
 *  http://www.gnu.org/copyleft/gpl.html                              *
 **********************************************************************/

using System;
//using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VideoLAN.LibVLC
{
    /**
     * @brief MediaHandle: unmanaged LibVLC media pointer
     * @ingroup Internals
     */
    internal sealed class MediaHandle : NonNullHandle
    {
        protected override void Destroy ()
        {
            LibVLC.MediaRelease (handle);
        }
    };

    /**
     * @brief Media: a source media
     * Use this class to extract meta-informations from a media.
     */
    public class Media : BaseObject
    {
        internal MediaHandle Handle
        {
            get
            {
                return handle as MediaHandle;
            }
        }

        /**
         * Creates a Media object.
         *
         * @param instance VLC instance
         * @param mrl Media Resource Locator (file path or URL)
         */
        public Media (VLC instance, string mrl)
        {
            U8String umrl = new U8String (mrl);

            handle = LibVLC.MediaCreate (instance.Handle, umrl, ex);
            Raise ();
        }

        public void AddOptions (string options, bool trusted)
        {
            U8String uopts = new U8String (options);

            if (trusted)
                LibVLC.MediaAddOption (Handle, uopts, ex);
            else
                LibVLC.MediaAddUntrustedOption (Handle, uopts, ex);
            Raise ();
        }
    };
};
