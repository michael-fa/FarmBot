# FarmBot :warning:  WIP
A bot for your free farm, aiming to do stuff automatic.

## :tv: [Streaming on YouTube!](https://www.youtube.com/watch?v=l3HopvETf3s)

❗ Note that doing bot stuff on your account will lead to an instant ban, once official game moderators have noticed.
This is a research and progression thing for me personally but it'll be released soon.

## Planned: 

### Presets / Action Templates 
The bot does check for basic stuff like if premium productions slots 
Are available and filling/retrieving them too.. but for fields / gardens and 
some other stuff we want user account specific acting and this is done via our own 
template language that are in plain english .txt format.
Default ones are shipped for a Level 1 account. 

#### Example template rules for harvest action
tmpl_farm_harvest.txt:\
`skip <plant_id>` will apply a rule that a specific plant is to be left on the field.

tmpl_farm_plant.txt:\
`plant <field_id> <plant_id>` will automatically plant given ID for a field. Use "all" as field_id to fill all until empty/max reached.\
`tryboost` Tries to shorten the growing phase for the current garden.\
`remove_if_obscurred <type: All, tree, mole, growth>` If enough money, each field will be cleared first and then cultivated.\


### Gardens/Standard fields
 - Auto harvesting
 - Auto planting
 - Auto watering
 - Auto Boost
 
### 🐑🐏🐮🐝🐟 Farms
 - Auto retrieve finished product
 - Auto feed (after template/preset)
