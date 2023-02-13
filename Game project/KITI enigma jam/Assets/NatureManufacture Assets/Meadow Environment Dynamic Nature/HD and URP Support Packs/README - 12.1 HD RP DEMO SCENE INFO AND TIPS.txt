About scene:

BEFORE YOU START:
- you need Unity  2021.3 or higher 
- you need HD SRP pipeline 12.1 if you use higher etc custom shaders could not work but seems they should. 
That's why we provide 12.1 version which seems to work with much higher versions aswell. 
For all higher RP versions please use 12.1 HD RP support pack.

Be patient this tech is so fluid... we coudn't follow every beta version

Step 1 
	- !!!! IMPORTANT !!!! Open "Project settings" ->"Gaphics"-> "HDRP global settings" ->  "Diffusion Profile Assets"
	and drag and drop our SSS settings diffusion profiles for foliage and water into Diffusion profile list:
		  NM_SSSSettings_Skin_Foliage
		  NM_SSSSettings_Skin_NM Foliage
		  NM_SSSSettings_Skin_NM Foliage Trees
	Without this foliage materials will not become affected by scattering and they will look wrong.
	Open "HDRPMediumQuality" in project settings or "HDRPHighQuality" depends what unity use i your projectas default and:
	- LOD Bias to = 1 or 1.5

Step 2 Go to project settings and quality and set:
	- Set VSync to don't sync

Setp 3 Find HD SRP Demo Small and open it.

Step 4 - HIT PLAY!:)

Play with it, give us feedback and learn about hd srp power.

