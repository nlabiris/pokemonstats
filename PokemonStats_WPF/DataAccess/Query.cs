using System;
using System.Collections.Generic;
using System.Data;

namespace PokemonStats_WPF.DataAccess {
    internal static class Query {
        public static string GetAllPokemon {
            get {
                return @"
SELECT `pokemon`.`id` AS `pokemon_id`, 
(
    SELECT `pokemon_species_names`.`name`
    FROM `pokemon_species_names`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon_species_names`.`pokemon_species_id`
    INNER JOIN `pokemon` ON `pokemon`.`species_id` = `pokemon_species`.`id`
    WHERE `pokemon_species_names`.`local_language_id` = 9
    AND `pokemon`.`id` = `pokemon_id`
    LIMIT 1
) AS `name`,
(
    SELECT `types`.`identifier`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    WHERE `pokemon_types`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1
) AS `type1`,
(
    SELECT `types`.`identifier`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    WHERE `pokemon_types`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1,1
) AS `type2`,
(
    SELECT `types`.`image`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    WHERE `pokemon_types`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1
) AS `type1_image`,
(
    SELECT `types`.`image`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    WHERE `pokemon_types`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1,1
) AS `type2_image`,
(
    SELECT `pokemon_color_names`.`name`
    FROM `pokemon_color_names`
    INNER JOIN `pokemon_colors` ON `pokemon_colors`.`id` = `pokemon_color_names`.`pokemon_color_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`color_id` = `pokemon_colors`.`id`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    AND `pokemon_color_names`.`local_language_id` = 9
    LIMIT 1
) AS `color`,
(
    SELECT `pokemon_shape_prose`.`name`
    FROM `pokemon_shape_prose`
    INNER JOIN `pokemon_shapes` ON `pokemon_shapes`.`id` = `pokemon_shape_prose`.`pokemon_shape_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`shape_id` = `pokemon_shapes`.`id`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    AND `pokemon_shape_prose`.`local_language_id` = 9
    LIMIT 1
) AS `shape`,
(
    SELECT `pokemon_habitat_names`.`name`
    FROM `pokemon_habitat_names`
    INNER JOIN `pokemon_habitats` ON `pokemon_habitats`.`id` = `pokemon_habitat_names`.`pokemon_habitat_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`habitat_id` = `pokemon_habitats`.`id`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    AND `pokemon_habitat_names`.`local_language_id` = 9
    LIMIT 1
) AS `habitat`,
(
    SELECT `pokemon_species`.`gender_rate`
    FROM `pokemon_species`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    LIMIT 1
) AS `gender_rate`,
(
    SELECT `pokemon_species`.`capture_rate`
    FROM `pokemon_species`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    LIMIT 1
) AS `capture_rate`,
(
    SELECT `pokemon_species`.`base_happiness`
    FROM `pokemon_species`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    LIMIT 1
) AS `base_happiness`,
(
    SELECT `growth_rate_prose`.`name`
    FROM `growth_rate_prose`
    INNER JOIN `growth_rates` ON `growth_rates`.`id` = `growth_rate_prose`.`growth_rate_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`growth_rate_id` = `growth_rates`.`id`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    AND `growth_rate_prose`.`local_language_id` = 9
    LIMIT 1
) AS `growth_rate`,
`pokemon`.`height`, 
`pokemon`.`weight`, 
`pokemon`.`base_experience`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    WHERE `pokemon_abilities`.`pokemon_id` = `pokemon`.`id`
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1
) AS `ability1`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    WHERE `pokemon_abilities`.`pokemon_id` = `pokemon`.`id`
    AND `pokemon_abilities`.`is_hidden` = 0
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1,1
) AS `ability2`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    WHERE `pokemon_abilities`.`pokemon_id` = `pokemon`.`id`
    AND `pokemon_abilities`.`is_hidden` = 1
    AND `ability_names`.`local_language_id` = 9
    LIMIT 2
) AS `hidden_ability`,
(
    SELECT `pokemon_species_flavor_text`.`flavor_text`
    FROM `pokemon_species_flavor_text`
    WHERE `pokemon_species_flavor_text`.`species_id` = `pokemon`.`species_id`
    AND `pokemon_species_flavor_text`.`version_id` = 21
    AND `pokemon_species_flavor_text`.`language_id` = 9
    LIMIT 1
) AS `species_summary`,
(
    SELECT `pokemon_forms`.`icon`
    FROM `pokemon_forms`
    WHERE `pokemon_forms`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1
) AS `icon`,
(
    SELECT `pokemon_forms`.`sprite`
    FROM `pokemon_forms`
    WHERE `pokemon_forms`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1
) AS `sprite`
FROM `pokemon`
WHERE `pokemon`.`id` <= 721;";
            }
        }

        public static string GetSpecificPokemon {
            get {
                return @"
SELECT `pokemon`.`id` AS `pokemon_id`, 
(
    SELECT `pokemon_species_names`.`name`
    FROM `pokemon_species_names`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon_species_names`.`pokemon_species_id`
    INNER JOIN `pokemon` ON `pokemon`.`species_id` = `pokemon_species`.`id`
    WHERE `pokemon_species_names`.`local_language_id` = 9
    AND `pokemon`.`id` = `pokemon_id`
    LIMIT 1
) AS `name`, 
(
    SELECT `types`.`identifier`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    WHERE `pokemon_types`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1
) AS `type1`,
(
    SELECT `types`.`identifier`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    WHERE `pokemon_types`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1,1
) AS `type2`,
(
    SELECT `pokemon_color_names`.`name`
    FROM `pokemon_color_names`
    INNER JOIN `pokemon_colors` ON `pokemon_colors`.`id` = `pokemon_color_names`.`pokemon_color_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`color_id` = `pokemon_colors`.`id`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    AND `pokemon_color_names`.`local_language_id` = 9
    LIMIT 1
) AS `color`,
(
    SELECT `pokemon_shape_prose`.`name`
    FROM `pokemon_shape_prose`
    INNER JOIN `pokemon_shapes` ON `pokemon_shapes`.`id` = `pokemon_shape_prose`.`pokemon_shape_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`shape_id` = `pokemon_shapes`.`id`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    AND `pokemon_shape_prose`.`local_language_id` = 9
    LIMIT 1
) AS `shape`,
(
    SELECT `pokemon_habitat_names`.`name`
    FROM `pokemon_habitat_names`
    INNER JOIN `pokemon_habitats` ON `pokemon_habitats`.`id` = `pokemon_habitat_names`.`pokemon_habitat_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`habitat_id` = `pokemon_habitats`.`id`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    AND `pokemon_habitat_names`.`local_language_id` = 9
    LIMIT 1
) AS `habitat`,
(
    SELECT `pokemon_species`.`gender_rate`
    FROM `pokemon_species`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    LIMIT 1
) AS `gender_rate`,
(
    SELECT `pokemon_species`.`capture_rate`
    FROM `pokemon_species`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    LIMIT 1
) AS `capture_rate`,
(
    SELECT `pokemon_species`.`base_happiness`
    FROM `pokemon_species`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    LIMIT 1
) AS `base_happiness`,
(
    SELECT `growth_rate_prose`.`name`
    FROM `growth_rate_prose`
    INNER JOIN `growth_rates` ON `growth_rates`.`id` = `growth_rate_prose`.`growth_rate_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`growth_rate_id` = `growth_rates`.`id`
    WHERE `pokemon_species`.`id` = `pokemon`.`species_id`
    AND `growth_rate_prose`.`local_language_id` = 9
    LIMIT 1
) AS `growth_rate`,
`pokemon`.`height`, 
`pokemon`.`weight`, 
`pokemon`.`base_experience`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    WHERE `pokemon_abilities`.`pokemon_id` = `pokemon`.`id`
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1
) AS `ability1`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    WHERE `pokemon_abilities`.`pokemon_id` = `pokemon`.`id`
    AND `pokemon_abilities`.`is_hidden` = 0
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1,1
) AS `ability2`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    WHERE `pokemon_abilities`.`pokemon_id` = `pokemon`.`id`
    AND `pokemon_abilities`.`is_hidden` = 1
    AND `ability_names`.`local_language_id` = 9
    LIMIT 2
) AS `hidden_ability`,
(
    SELECT `pokemon_species_flavor_text`.`flavor_text`
    FROM `pokemon_species_flavor_text`
    WHERE `pokemon_species_flavor_text`.`species_id` = `pokemon`.`species_id`
    AND `pokemon_species_flavor_text`.`version_id` = 21
    AND `pokemon_species_flavor_text`.`language_id` = 9
    LIMIT 1
) AS `species_summary`,
(
    SELECT `pokemon_forms`.`icon`
    FROM `pokemon_forms`
    WHERE `pokemon_forms`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1
) AS `icon`,
(
    SELECT `pokemon_forms`.`sprite`
    FROM `pokemon_forms`
    WHERE `pokemon_forms`.`pokemon_id` = `pokemon`.`id`
    LIMIT 1
) AS `sprite`
FROM `pokemon`
WHERE `pokemon`.`id` = @pokemon_id";
            }
        }

        public static string GetSpecificSpecies {
            get {
                return @"
        SELECT `id`, 
        `identifier`, 
        `generation_id`, 
        `evolves_from_species_id`, 
        `evolution_chain_id`, 
        `color_id`, 
        `shape_id`, 
        `habitat_id`, 
        `gender_rate`, 
        `capture_rate`, 
        `base_happiness`, 
        `is_baby`, 
        `hatch_counter`, 
        `has_gender_differences`, 
        `growth_rate_id`, 
        `forms_switchable`, 
        `order`, 
        `conquest_order`
        FROM `pokemon_species`
        WHERE `id` = @species_id";
            }
        }

        public static string GetSpecificGeneration {
            get {
                return @"
    SELECT `id`,
    `main_region_id`,
    `identifier`
    FROM `generations`
    WHERE `id` = @generations_id";
            }
        }

        public static string GetSpecificRegion {
            get {
                return @"
        SELECT `id`,
        `identifier`
        FROM `regions`
        WHERE `id` = @regions_id";
            }
        }

        public static string GetSpecificColor {
            get {
                return @"
    SELECT `id`,
    `identifier`
    FROM `pokemon_colors`
    WHERE `id` = @color_id";
            }
        }

        public static string GetSpecificShape {
            get {
                return @"
    SELECT `id`,
    `identifier`
    FROM `pokemon_shapes`
    WHERE `id` = @shape_id";
            }
        }

        public static string GetSpecificHabitat {
            get {
                return @"
    SELECT `id`,
    `identifier`
    FROM `pokemon_habitats`
    WHERE `id` = @habitat_id";
            }
        }

        public static string GetSpecificGrowthRate {
            get {
                return @"
    SELECT `id`,
    `identifier`,
    `formula`
    FROM `growth_rates`
    WHERE `id` = @growth_rate_id";
            }
        }

        public static string GetSpecificEvolutionChain {
            get {
                return @"
        SELECT `id`,
        `baby_trigger_item_id`
        FROM `evolution_chains`
        WHERE `id` = @evolution_chains_id";
            }
        }

        public static string GetSpecificBabyTriggerItem {
            get {
                return @"SELECT `id`, 
        `identifier`, 
        `category_id`, 
        `cost`, 
        `fling_power`, 
        `fling_effect_id`
        FROM `items`
        WHERE `id` = @baby_trigger_item_id";
            }
        }

        public static string GetSpecificCategory {
            get {
                return @"
            SELECT `id`,
            `pocket_id`,
            `identifier`
            FROM `item_categories`
            WHERE `id` = @category_id";
            }
        }

        public static string GetSpecificPocket {
            get {
                return @"
                SELECT `id`,
                `identifier`
                FROM `item_pockets`
                WHERE `id` = @pocket_id";
            }
        }

        public static string GetSpecificFlingEffect {
            get {
                return @"
            SELECT `id`
            FROM `item_fling_effects`
            WHERE `id` = @fling_effect_id";
            }
        }

        public static string GetAllAbilities {
            get {
                return @"
                SELECT `id`,
                `identifier`,
                `generation_id`,
                `is_main_series`
FROM `abilities`";
            }
        }

        public static string GetSpecificAbility {
            get {
                return @"
                SELECT `id`,
                `identifier`,
                `generation_id`,
                `is_main_series`
FROM `abilities`
WHERE `id` = @ability_id";
            }
        }

        public static string GetSpecificAbilityChangelog {
            get {
                return @"
                SELECT `id`,
                `ability_id`,
                `changed_in_version_group_id`
FROM `ability_changelog`
WHERE `ability_id` = @ability_id";
            }
        }

        public static string GetSpecificVersionGroup {
            get {
                return @"
SELECT `id`, 
`identifier`, 
`generation_id`, 
`order` 
FROM `version_groups` 
WHERE `id` = @version_group_id";
            }
        }

        public static string GetSpecificAbilityChangelogProse {
            get {
                return @"
                SELECT `ability_changelog_id`,
                `local_language_id`,
                `effect`
                FROM `ability_changelog_prose`
                WHERE `ability_changelog_id` = @ability_changelog_id";
            }
        }

        public static string GetSpecificLanguage {
            get {
                return @"
                SELECT `id`, 
                `iso639`, 
                `iso3166`, 
                `identifier`, 
                `official`, 
                `order`
                FROM `languages`
                WHERE `id` = @language_id";
            }
        }

        public static string GetSpecificAbilityName {
            get {
                return @"
                SELECT `ability_id`,
                `local_language_id`,
                `name`
                FROM `ability_names`
                WHERE `ability_id` = @ability_id";
            }
        }

        public static string GetSpecificAbilityProse {
            get {
                return @"
                SELECT `local_language_id`,
                `short_effect`,
                `effect`
                FROM `ability_prose`
                WHERE `ability_id` = @ability_id";
            }
        }

        public static string GetAllBerries {
            get {
                return @"
                SELECT `id`, 
                `item_id`, 
                `firmness_id`, 
                `natural_gift_power`, 
                `natural_gift_type_id`, 
                `size`, 
                `max_harvest`, 
                `growth_time`, 
                `soil_dryness`, 
                `smoothness` 
                FROM `berries`";
            }
        }

        public static string GetSpecificBerry {
            get {
                return @"
                SELECT `id`, 
                `item_id`, 
                `firmness_id`, 
                `natural_gift_power`, 
                `natural_gift_type_id`, 
                `size`, 
                `max_harvest`, 
                `growth_time`, 
                `soil_dryness`, 
                `smoothness` 
                FROM `berries`
                WHERE `id` = @berry_id";
            }
        }

        public static string GetSpecificItem {
            get {
                return @"
                SELECT `id`, 
                `identifier`, 
                `category_id`, 
                `cost`, 
                `fling_power`, 
                `fling_effect_id`
                FROM `items`
                WHERE `id` = @item_id";
            }
        }

        public static string GetSpecificBerryFirmness {
            get {
                return @"
                SELECT `id`,
                `identifier`
                FROM `berry_firmness`
                WHERE `id` = @firmness_id";
            }
        }

        public static string GetAllTypes {
            get {
                return @"
                SELECT `id`,
                `identifier`,
                `generation_id`,
                `damage_class_id`
                FROM `types`";
            }
        }

        public static string GetSpecificType {
            get {
                return @"
                SELECT `id`,
                `identifier`,
                `generation_id`,
                `damage_class_id`
                FROM `types`
                WHERE `id` = @type_id";
            }
        }

        public static string GetSpecificDamageClass {
            get {
                return @"
                SELECT `id`,
                `identifier`
                FROM `move_damage_classes`
                WHERE `id` = @damage_class_id";
            }
        }

        public static string GetSpecificBerryFirmnessName {
            get {
                return @"
                SELECT `local_language_id`,
                `name`
                FROM `berry_firmness_names`
                WHERE `berry_firmness_id` = @firmness_id";
            }
        }

        public static string GetSpecificBerryFlavor {
            get {
                return @"
                SELECT `contest_type_id`,
                `flavor`
                FROM `berry_flavors`
                WHERE `berry_id` = @berry_id";
            }
        }

        public static string GetSpecificContestType {
            get {
                return @"
                SELECT `id`,
                `identifier`
                FROM `contest_types`
                WHERE `id` = @contest_type_id";
            }
        }
    }
}