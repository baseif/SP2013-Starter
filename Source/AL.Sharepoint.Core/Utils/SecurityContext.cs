﻿using System;
using System.Security.Principal;

namespace AL.Sharepoint.Core.Utils
{
    internal class SecurityContext : IDisposable
    {
        WindowsImpersonationContext _ctx;
        private SecurityContext()
        {
            UseAppPoolIdentity();
        }

        private void UseAppPoolIdentity()
        {
            try
            {
                if (!WindowsIdentity.GetCurrent().IsSystem)
                {
                    _ctx = WindowsIdentity.Impersonate(IntPtr.Zero);
                }
            }
            catch { }
        }

        private void ReturnToCurrentUser()
        {
            try
            {
                if (_ctx != null)
                {
                    _ctx.Undo();
                }
            }
            catch { }
        }
        public void Dispose()
        {
            ReturnToCurrentUser();
        }
    }
}