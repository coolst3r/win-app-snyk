﻿/*
 * Copyright (c) 2022 Proton
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

using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtonVPN.UI.Test.FlaUI.Utils;

namespace ProtonVPN.UI.Test.FlaUI
{
    public class FlaUIActions : TestSession
    {
        protected dynamic WaitUntilElementExistsByName(string name, TimeSpan time)
        {
            RetryResult<AutomationElement> retry = Retry.WhileNull(
                () => {
                    App.WaitWhileBusy();
                    RefreshWindow();
                    return Window.FindFirstDescendant(cf => cf.ByName(name));
                },
                time, TestConstants.RetryInterval);

            if (!retry.Success)
            {
                Assert.Fail("Failed to get " + name + " element within " + time.Seconds + " seconds.");
            }
            return this;
        }

        protected dynamic WaitUntilDisplayedByAutomationId(string automationId, TimeSpan time)
        {
            RetryResult<bool> retry = Retry.WhileTrue(
                () => {
                    RefreshWindow();
                    if(Window.FindFirstDescendant(cf => cf.ByAutomationId(automationId)) == null)
                    {
                        return true;
                    }
                    else
                    {
                        return Window.FindFirstDescendant(cf => cf.ByAutomationId(automationId)).IsOffscreen;
                    }
                },
                time, TestConstants.RetryInterval);

            if (!retry.Success)
            {
                Assert.Fail("Failed to get " + automationId + "element within " + time.Seconds + " seconds.");
            }
            return this;
        }

        protected dynamic WaitUntilElementExistsByAutomationId(string automationId, TimeSpan time)
        {
            RetryResult<AutomationElement> retry = Retry.WhileNull(
                () => {
                    RefreshWindow();
                    return Window.FindFirstDescendant(cf => cf.ByAutomationId(automationId));
                },
                time, TestConstants.RetryInterval);

            if (!retry.Success)
            {
                Assert.Fail("Failed to get " + automationId + "element within " + time.Seconds + " seconds.");
            }
            return this;
        }

        protected dynamic WaitUntilElementExistsByClassName(string className, TimeSpan time)
        {
            RetryResult<AutomationElement> retry = Retry.WhileNull(
                () => {
                    RefreshWindow();
                    return Window.FindFirstDescendant(cf => cf.ByClassName(className));
                },
                time, TestConstants.RetryInterval);

            if (!retry.Success)
            {
                Assert.Fail("Failed to get " + className + "element within " + time.Seconds + " seconds.");
            }
            return this;
        }

        protected dynamic WaitUntilTextMatchesByAutomationId(string automationId, TimeSpan time, string text, string timeoutMessage)
        {
            Retry.WhileFalse(
                () => {
                    RefreshWindow();
                    return ElementByAutomationId(automationId).AsLabel().Text == text;
                },
                time, TestConstants.RetryInterval, true, false, timeoutMessage);
            return this;
        }

        protected dynamic CheckIfDisplayedByClassName(string className)
        {
            RefreshWindow();
            Assert.IsFalse(Window.FindFirstDescendant(cf => cf.ByClassName(className)).IsOffscreen);
            return this;
        }

        protected dynamic CheckIfNotDisplayedByName(string name)
        {
            RefreshWindow();
            Assert.IsTrue(Window.FindFirstDescendant(cf => cf.ByName(name)).IsOffscreen);
            return this;
        }

        protected dynamic CheckIfExistsByAutomationId(string automationId)
        {
            RefreshWindow();
            Assert.IsNotNull(Window.FindFirstDescendant(cf => cf.ByAutomationId(automationId)));
            return this;
        }

        protected dynamic MoveMouseToElement(AutomationElement element, int offsetX = 0, int offsetY = 0)
        {
            Mouse.MovePixelsPerMillisecond = 100;
            Mouse.MoveTo(element.GetClickablePoint().X + offsetX, element.GetClickablePoint().Y + offsetY);
            return this;
        }

        protected AutomationElement ElementByAutomationId(string automationId)
        {
            WaitUntilElementExistsByAutomationId(automationId, TestConstants.ShortTimeout);
            return Window.FindFirstDescendant(cf => cf.ByAutomationId(automationId));
        }

        protected AutomationElement ElementByClassName(string className)
        {
            WaitUntilElementExistsByClassName(className, TestConstants.ShortTimeout);
            return Window.FindFirstDescendant(cf => cf.ByClassName(className));
        }

        protected AutomationElement ElementByName(string name)
        {
            WaitUntilElementExistsByName(name, TestConstants.ShortTimeout);
            return Window.FindFirstDescendant(cf => cf.ByName(name));
        }
    }
}