﻿using System.Collections.Generic;

namespace GGJ.Data 
{
	public class CharacterInfo
	{
		public Type   CharacterType { get; protected set; }
		public string Name { get; protected set; }
		//public string Bio { get; protected set; }
		public string SpriteResource { get; protected set; }
		public string PortraitResource { get; protected set; }

		private CharacterInfo() { }

		public enum Type
		{
			Robot,
			RykerZane,
			ChelseaJackson,
			MaraQuinn
		};

		public static CharacterInfo Robot()
		{
			CharacterInfo charInfo = new CharacterInfo();
			charInfo.Name = "Designation 442";
			charInfo.CharacterType = Type.Robot;
            charInfo.PortraitResource = "Sprites/Robot/Head_Guy";
            charInfo.SpriteResource = "SpriterObjects/Robot";
			return charInfo;
		}

		public static CharacterInfo Engineer()
		{
			CharacterInfo charInfo = new CharacterInfo();
			charInfo.Name = "Mara Quinn";
            charInfo.CharacterType = Type.MaraQuinn;
            charInfo.PortraitResource = "Sprites/Mara/Head_Guy";
            charInfo.SpriteResource = "SpriterObjects/MaraQuinn";
			return charInfo;
		}

		public static CharacterInfo Navigator()
		{
			CharacterInfo charInfo = new CharacterInfo();
			charInfo.Name = "Chelsea Jackson";
            charInfo.CharacterType = Type.ChelseaJackson;
            charInfo.PortraitResource = "Sprites/Chelsea/Head_Guy";
            charInfo.SpriteResource = "SpriterObjects/ChelseaJackson";
			return charInfo;
		}

		public static CharacterInfo Security()
		{
			CharacterInfo charInfo = new CharacterInfo();
			charInfo.Name = "Ryker Zane";
			charInfo.CharacterType = Type.RykerZane;
            charInfo.PortraitResource = "Sprites/Guy/Head_Guy";
            charInfo.SpriteResource = "SpriterObjects/RykerZane";
			return charInfo;
		}

	}
}