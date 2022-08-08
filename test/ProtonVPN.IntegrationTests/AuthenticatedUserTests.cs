﻿/*
 * Copyright (c) 2022 Proton Technologies AG
 *
 * This file is part of ProtonVPN.
 *
 * ProtonVPN is free software:
 you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ProtonVPN is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with ProtonVPN.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Net;
using System.Security;
using System.Threading.Tasks;
using ProtonVPN.Core.Auth;
using ProtonVPN.Core.Srp;
using ProtonVPN.IntegrationTests.Api.Responses;

namespace ProtonVPN.IntegrationTests
{
    public class AuthenticatedUserTests : TestBase
    {
        protected const string CORRECT_PASSWORD = "password";
        protected const string WRONG_PASSWORD = "wrong";

        protected void SetApiResponsesForAuth()
        {
            Api.SetAuthInfoResponse(new AuthInfoResponseMock());
            Api.SetAuthResponse(new AuthResponseMock());
            Api.SetVpnInfoResponse(new VpnInfoWrapperResponseMock());
            Api.SetAuthCertificateResponse(new CertificateResponseMock());
        }

        protected async Task<AuthResult> MakeUserAuth(string password)
        {
            SrpPInvoke.SetUnitTest();
            SecureString securePassword = new NetworkCredential("", password).SecurePassword;
            return await Resolve<UserAuth>().LoginUserAsync("username", securePassword);
        }
    }
}