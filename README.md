# Thermal Fracturing

This is a Vintage Story mode which allows you to break rocks with a hot firepit.

## Quickstart

The recommended install method is via the official Mod DB. To run a local build, run 
the "Launch Client (Debug)" launch configuration in VSCode.

The mod is very simple. Just burn a firepit loaded with charcoal adjacent to some rocks and wait patiently while the heat does its work!

## Background

Here's a ChatGPT excerpt I plagiarized describing "fire setting" mining (AKA thermal fracturing):

> Fire setting, an ancient mining technique, harnesses the elemental power of fire to extract valuable minerals from the earth. Predominantly used in prehistoric times and continuing into the early modern era, this method involved heating rock faces with large fires, then rapidly cooling them with water or vinegar. This sudden temperature change caused the rock to crack and fracture, facilitating easier extraction of ores. Fire setting was especially prevalent in mining operations for metals like gold, copper, and tin, where conventional tools of the era were insufficient to penetrate hard rock formations. The technique reflects early human ingenuity in overcoming natural barriers, although it was labor-intensive and posed significant risks, including the potential for uncontrolled fires and toxic fumes. Over time, with advancements in technology and an increased understanding of geology, fire setting was largely replaced by more efficient and safer mining methods. However, its historical significance lies in its contribution to early metallurgical developments and the shaping of mining practices.

See also: the [Wikipedia article](https://en.wikipedia.org/wiki/Fire-setting).

## Development

Recommended IDE is VSCode. The provided "Launch Client (Debug)" configuration will run your 
Vintage Story install with this mod loaded and the debugger attached. If you run into some 
unexpected jank (especially if you feel like your recent changes aren't loaded into the game) delete
the `ThermalFracturing/bin` directory and rerun the launch config.

This repo uses the [vsmod dotnet template](https://wiki.vintagestory.at/index.php/Modding:Setting_up_your_Development_Environment#Mod_Template_package).
