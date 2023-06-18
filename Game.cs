using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Elementaria
{
    public class Game
    {
        public int CurrentLVL = 0;
        public ICreature[,] Map;
        public Keys KeyPressed;
        public int MapWidth => Map.GetLength(0);
        public int MapHeight => Map.GetLength(1);
        public void CreateMap(string map)
        {
            Map = MapCreator.CreateMap(map);
        }
        public int PlayerSpeed = 2;
        public string[] LVLs = { @"
PSSSB BS            B    
   SCB    SS  SSSSSSS SB 
   SB  S SSS BSSSSSSS S  
   SSSSS SSSB SSSSSSS SS 
  B  SSS SSS   CBS B B  B
SS S SSS SSSSSSBBS SSSSS 
 SBS SSSBB    BBB      S 
 S S SSS SSSS B    SS  S 
 S S SSSSSSSS SSSS SSS S 
   S BB     S SSSS SSSSS 
  BS  SSS  B    SS  SS   
S SS  B SSS SSB S   S  SS
S    BBB BS SS  SS SS  SS
S SBSS    SB  B SBBB   SS
SSSSSSSSS   SSS SS SSSSSS
", @"
S         B  B 
S      SSSS    
S SSSSB   SSSS 
SBS   BB  S  S 
 B  S SSSBS  S 
S SSS S  BSB S 
S S S SPSSSB   
  B   B  B   SS
 SS SSSSSB S SS
 SS        S SS
 SSSSSS SS S SS
 B  B B    SB  
SSSS  SSSSSS BB
      B    B   
SSSSSSS   B B C 
", @"
PB     SSS     
BSSSSB SSS BS  
 B  S BSSSB SB 
 B  SB SSS  S B
SSS SB   B  S B
SSS S   B   S  
SSS  BSSSSS S  
SSS   SSSSS S  
S  BSSSSSSS SB 
S S SSSSSSS S B
S S BB      SB 
S S        SS  
S SSSSSSS  SS B
S        B SS C
SSSSSSSSSSSSS B
", @"
PSSSS   B   S B       SSS
   SS SSSS SS SSSSSSB SSS
SS SS S    S B  SSSS BS  
SSB   S    S SB  SSS  S  
SS   B  BSSS S S B  B S  
SS SSSSS       SSSSS  SBB
   SSSSS SSSSS B  B  B   
 S SSSSSBSSSSS  S SSB    
 SB B    SSSSSB S SS BSSB
 B    B         S     SS 
SSSSSSSSSSSSSSSSS SSSSSS 
 B  B B    B     B     B 
 SCS SSS SSS BSSSS BSSSS 
 SSS SSS SSSS    S  SSSSB
         SSSSSB  B  SSSSS
"
        };
    }
}
