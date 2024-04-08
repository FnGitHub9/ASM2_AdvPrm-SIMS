﻿using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASM2_AdvPrm_SIMS.Service
{
    public class AdminService
    {
        private readonly AdminContext _adminContext;
        public IList<Admin> Administrators { get; set; }

        public AdminService(AdminContext adminContext)
        {
            _adminContext = adminContext;
            Administrators = GetAdministrators();
        }

        public IList<Admin> GetAdministrators()
        {
            return _adminContext.Administrators.ToList();
        }

        public void AddAdmin(Admin admin)
        {
            _adminContext.AddAdmin(admin);
        }

        public void UpdateAdmin(int id, Admin admin)
        {
            _adminContext.UpdateAdmin(id, admin);
        }

        public void DeleteAdmin(int id)
        {
            var admin = _adminContext.Administrators.Find(a => a.Id == id);
            if (admin != null)
            {
                _adminContext.DeleteAdmin(id);
            }
        }
    }
}