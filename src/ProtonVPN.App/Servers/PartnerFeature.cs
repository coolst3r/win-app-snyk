﻿/*
 * Copyright (c) 2023 Proton AG
 *
 * This file is part of ProtonVPN.
 *
 * ProtonVPN is free software: you can redistribute it and/or modify
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

using System.Collections.Generic;
using ProtonVPN.Partners;
using ProtonVPN.Translations;

namespace ProtonVPN.Servers
{
    public class PartnerFeature : IServerFeature
    {
        public PartnerFeature(string name, string iconUrl, List<PartnerType> partnerTypes)
        {
            Name = name;
            IconUrl = iconUrl;
            InfoPopupViewModel = new InfoPopupViewModel(new FreeServersInfoPopupViewModel(partnerTypes),
                Translation.Format("Sidebar_Countries_Information"));
        }

        public string Name { get; }
        public string IconUrl { get; }
        public InfoPopupViewModel InfoPopupViewModel { get; }
    }
}