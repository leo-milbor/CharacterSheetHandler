﻿using CharacterSheetHandler.Models.Vampire;

using FsCheck;
using FsCheck.Xunit;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CharacterSheetHandler.Models.Tests.Vampire
{
    public class SkillTests
    {
        [Property(Arbitrary = new[] { typeof(NameArbitrary), typeof(LevelArbitrary) } )]
        public Property CanCreate_Skill_From_LevelAndName(Name name, Level level)
        {
            var skill = Skill.New(name, level);

            return (skill is Result<Skill, SkillError>.Ok ok
                && ((Skill)ok).Name == name
                && ((Skill)ok).Level == level)
                .ToProperty();
        }

        [Property(Arbitrary = new [] { typeof(LevelArbitrary)})]
        public Property NullName_To_EmptyNameError(Level level)
        {
            var skill = Skill.New(null, level);

            return (skill is Result<Skill, SkillError>.Error error
                && (SkillError)error is SkillError.EmptyNameError)
                .ToProperty();
        }

        [Property(Arbitrary = new[] { typeof(NameArbitrary) })]
        public Property NullLevel_To_NoLevelError(Name name)
        {
            var skill = Skill.New(name, null);

            return (skill is Result<Skill, SkillError>.Error error
                && (SkillError)error is SkillError.NoLevelError)
                .ToProperty();
        }
    }
}