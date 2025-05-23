﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.IUserRole;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Domain.Models;

namespace Shop.Application.Features.User.Handlers.Queries
{
    public class UserRoleListQueryHandler : IRequestHandler<UserRoleListQueryReq, List<IUserRoleDto>>
    {
        #region Constractor

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleRepository _roleRep;
        private readonly IUserRepository _userRep;

        public UserRoleListQueryHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IRoleRepository roleRep, IUserRepository userRep)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRep = roleRep;
            _userRep = userRep;
        }

        #endregion
        public async Task<List<IUserRoleDto>> Handle(UserRoleListQueryReq request, CancellationToken cancellationToken)
        {
            return await _userRep.GetUserRoles(request.userId);
        }
    }

    public class UserRoleCreateQueryHandler : IRequestHandler<UserRoleCreateQueryReq, Unit>
    {
        #region Constractor

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleRepository _roleRep;
        private readonly IUserRepository _userRep;
        private readonly IUserCateguryRepository _userCatRep;

        public UserRoleCreateQueryHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IRoleRepository roleRep, IUserRepository userRep, IUserCateguryRepository userCatRep)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRep = roleRep;
            _userRep = userRep;
            _userCatRep = userCatRep;
        }

        #endregion
        public async Task<Unit> Handle(UserRoleCreateQueryReq request, CancellationToken cancellationToken)
        {
            var muser = await _userRep.GetAsync(request.IUserRoleDto.NumberuserId);
            var user = await _userManager.FindByNameAsync(muser.UserName);
            await _userManager.AddToRoleAsync(user, request.IUserRoleDto.RoleName);
            if (request.IUserRoleDto.RoleName == "BankCustomer")
            {
                var existUserInCategury = await _userCatRep.ExistUserInCategury(muser.Id);
                if (!existUserInCategury)
                {
                    UserCategury cat = new UserCategury()
                    {
                        UserId = muser.Id,
                        CateguryId=1

                    };
                    await _userCatRep.AddAsync(cat);
                }
            }
            return Unit.Value;
        }
    }

    public class UserRoleDeleteQueryHandler : IRequestHandler<UserRoleDeleteQueryReq, Unit>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleRepository _roleRep;
        private readonly IUserRepository _userRep;
        private readonly IUserCateguryRepository _userCatRep;

        public UserRoleDeleteQueryHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IRoleRepository roleRep, IUserRepository userRep, IUserCateguryRepository userCatRep)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRep = roleRep;
            _userRep = userRep;
            _userCatRep = userCatRep;
        }
        public async Task<Unit> Handle(UserRoleDeleteQueryReq request, CancellationToken cancellationToken)
        {
            var muser = await _userRep.GetUserByUserName(request.IUserRoleDto.UserName);
            var user = await _userManager.FindByNameAsync(request.IUserRoleDto.UserName);
            await _userManager.RemoveFromRoleAsync(user, request.IUserRoleDto.RoleName);
            if (request.IUserRoleDto.RoleName == "BankCustomer")
            {
                var existUserInCategury = await _userCatRep.ExistUserInCategury(muser.Id);
                if (existUserInCategury)
                {
                    var uc =await _userCatRep.GetUserCateguryByUserId(muser.Id);
                    await _userCatRep.DeleteAsync(uc);
                }
            }
            return Unit.Value;
        }
    }
}
