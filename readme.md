# Description

Append symbols to location names that are involved in quests. Based on the script from GRUmod [Quest Dungeons Marked (.ESPless)](https://www.nexusmods.com/oblivionremastered/mods/3560). 

## Install

- Manually download [MagicLoader](https://www.nexusmods.com/oblivionremastered/mods/1966?tab=description) **2.4+** and extract it to:
    - Steam: `~\Oblivion Remastered\MagicLoader\`
    - GamePass: `~\The Elder Scrolls IV- Oblivion Remastered\Content\MagicLoader\`
- Download and extract your flavour of the mod.
- Run `MagicLoader.exe` (version 2 will now start the game unless you create a shortcut and add -c as a parameter).
- Launch the game, if necessary depending on the previous step.

## Uninstall

- Delete `MarkQuestDungeons.json` from `~\OblivionRemastered_*\Content\Dev\ObvData\Data\MagicLoader\`
- Delete contents of: `\OblivionRemastered_*\Content\Paks\~mods\MagicLoader\`
- Run `MagicLoader.exe` if you have other mods that depend on it, skip this step if not.
- Launch the game, if necessary.

## Update
- Overwrite *MarkQuestDungeons.json* in `\OblivionRemastered_*\Content\Dev\ObvData\Data\MagicLoader\`
- Run `MagicLoader.exe`.
- Launch the game, if necessary.

## Notes
- You must re-run [MagicLoader](https://www.nexusmods.com/oblivionremastered/mods/1966?tab=description) **2.4+** if I send out any updates to apply them!
- Safe mid-playthrough, but ALWAYS make backups when adding new mods!
- Vortex should be able to put my mod in the correct place for you with the `install` button, but you still need to manually install [MagicLoader](https://www.nexusmods.com/oblivionremastered/mods/1966?tab=description) and inject the files after installing it.

## Credits
- GRUmod for the list of locations found in his UE4SS version [Quest Dungeons Marked (.ESPless)](https://www.nexusmods.com/oblivionremastered/mods/3560).
- RareMojo for the mod description I shamelessly copied from [Tools First - Inventory Sorting](https://www.nexusmods.com/oblivionremastered/mods/964) .

# Changelog
## Version 1.0.0
- First stable release
## Version 1.0.1
- Update the MagicLoaderGenerator library to version 1.2.1 to fix problems with the generated Zip archives
## Version 1.1
- Added 53 interior locations related to quests
## Version 1.1.1
- Added 3 interior locations related to quests
## Version 2.0.0
- Update for MagicLoader version 2
