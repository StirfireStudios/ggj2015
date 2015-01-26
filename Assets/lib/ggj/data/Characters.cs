using System.Collections.Generic;

namespace GGJ.Data
{
	public class Characters
	{
		static Characters instance;
		static public Characters Instance
		{
			get
			{
				if (instance == null)
					instance = new Characters();
				return instance;
			}
		}	

		public Dictionary<CharacterInfo.Type, CharacterInfo> CharacterList { get; protected set; }
		public CharacterInfo.Type[] Types { get; protected set; }

		private Characters()
		{
			CharacterList = new Dictionary<CharacterInfo.Type, CharacterInfo> ();
			CharacterInfo temp;

			temp = CharacterInfo.Robot ();
			CharacterList [temp.CharacterType] = temp;

			temp = CharacterInfo.Engineer ();
			CharacterList [temp.CharacterType] = temp;

			temp = CharacterInfo.Navigator ();
			CharacterList [temp.CharacterType] = temp;

			temp = CharacterInfo.Security ();
			CharacterList [temp.CharacterType] = temp;

			Types = new CharacterInfo.Type[CharacterList.Count];
			int index = 0;
			foreach (CharacterInfo.Type type in CharacterList.Keys)
			{
				Types[index] = type;
				index += 1;
			}
		}

	}
}

