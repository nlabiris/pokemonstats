namespace PokemonStats_WPF.DataAccess {
    internal static class Query {
        public static string GetAllPokemon {
            get {
                return @"
SELECT `pokemon_species`.`id` AS `index_number`, 
(
    SELECT `pokemon_species_names`.`name`
    FROM `pokemon_species_names`
    WHERE `pokemon_species_names`.`local_language_id` = 9
    AND `pokemon_species_names`.`pokemon_species_id` = `pokemon_species`.`id`
    LIMIT 1
) AS `name`,
(
    SELECT `types`.`identifier`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_types`.`pokemon_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon`.`species_id`
    WHERE `pokemon_species`.`id` = `index_number`
    LIMIT 1
) AS `type1`,
(
    SELECT `types`.`identifier`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_types`.`pokemon_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon`.`species_id`
    WHERE `pokemon_species`.`id` = `index_number`
    LIMIT 1,1
) AS `type2`,
(
    SELECT `types`.`image`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_types`.`pokemon_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon`.`species_id`
    WHERE `pokemon_species`.`id` = `index_number`
    LIMIT 1
) AS `type1_image`,
(
    SELECT `types`.`image`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_types`.`pokemon_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon`.`species_id`
    WHERE `pokemon_species`.`id` = `index_number`
    LIMIT 1,1
) AS `type2_image`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `hp`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1,1
) AS `atk`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 2,1
) AS `def`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 3,1
) AS `sp_atk`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 4,1
) AS `sp_def`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 5,1
) AS `spe`,
(
    SELECT `pokemon_color_names`.`name`
    FROM `pokemon_color_names`
    INNER JOIN `pokemon_colors` ON `pokemon_colors`.`id` = `pokemon_color_names`.`pokemon_color_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`color_id` = `pokemon_colors`.`id`
    WHERE `pokemon_species`.`id` = `index_number`
    AND `pokemon_color_names`.`local_language_id` = 9
    LIMIT 1
) AS `color`,
(
    SELECT `pokemon_shape_prose`.`name`
    FROM `pokemon_shape_prose`
    INNER JOIN `pokemon_shapes` ON `pokemon_shapes`.`id` = `pokemon_shape_prose`.`pokemon_shape_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`shape_id` = `pokemon_shapes`.`id`
    WHERE `pokemon_species`.`id` = `index_number`
    AND `pokemon_shape_prose`.`local_language_id` = 9
    LIMIT 1
) AS `shape`,
(
    SELECT `pokemon_habitat_names`.`name`
    FROM `pokemon_habitat_names`
    INNER JOIN `pokemon_habitats` ON `pokemon_habitats`.`id` = `pokemon_habitat_names`.`pokemon_habitat_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`habitat_id` = `pokemon_habitats`.`id`
    WHERE `pokemon_species`.`id` = `index_number`
    AND `pokemon_habitat_names`.`local_language_id` = 9
    LIMIT 1
) AS `habitat`,
`gender_rate`,
`capture_rate`,
`base_happiness`,
(
    SELECT `growth_rate_prose`.`name`
    FROM `growth_rate_prose`
    INNER JOIN `growth_rates` ON `growth_rates`.`id` = `growth_rate_prose`.`growth_rate_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`growth_rate_id` = `growth_rates`.`id`
    WHERE `pokemon_species`.`id` = `index_number`
    AND `growth_rate_prose`.`local_language_id` = 9
    LIMIT 1
) AS `growth_rate`,
(
    SELECT `pokemon`.`height`
    FROM `pokemon`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `height`,
(
    SELECT `pokemon`.`weight`
    FROM `pokemon`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `weight`,
(
    SELECT `pokemon`.`base_experience`
    FROM `pokemon`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `base_experience`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_abilities`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1
) AS `ability1`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_abilities`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    AND `pokemon_abilities`.`is_hidden` = 0
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1,1
) AS `ability2`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_abilities`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    AND `pokemon_abilities`.`is_hidden` = 1
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1
) AS `hidden_ability`,
(
    SELECT `pokemon_species_flavor_text`.`flavor_text`
    FROM `pokemon_species_flavor_text`
    WHERE `pokemon_species_flavor_text`.`species_id` = `index_number`
    AND `pokemon_species_flavor_text`.`version_id` = 21
    AND `pokemon_species_flavor_text`.`language_id` = 9
    LIMIT 1
) AS `species_summary`,
(
    SELECT `pokemon_forms`.`icon`
    FROM `pokemon_forms`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_forms`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `icon`,
(
    SELECT `pokemon_forms`.`sprite`
    FROM `pokemon_forms`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_forms`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `sprite`
FROM `pokemon_species`
ORDER BY `index_number`;";
            }
        }

        public static string GetSpecificPokemon {
            get {
                return @"
SELECT `pokemon_species`.`id` AS `index_number`, 
(
    SELECT `pokemon_species_names`.`name`
    FROM `pokemon_species_names`
    WHERE `pokemon_species_names`.`local_language_id` = 9
    AND `pokemon_species_names`.`pokemon_species_id` = `pokemon_species`.`id`
    LIMIT 1
) AS `name`,
(
    SELECT `types`.`identifier`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_types`.`pokemon_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon`.`species_id`
    WHERE `pokemon_species`.`id` = `index_number`
    LIMIT 1
) AS `type1`,
(
    SELECT `types`.`identifier`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_types`.`pokemon_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon`.`species_id`
    WHERE `pokemon_species`.`id` = `index_number`
    LIMIT 1,1
) AS `type2`,
(
    SELECT `types`.`image`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_types`.`pokemon_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon`.`species_id`
    WHERE `pokemon_species`.`id` = `index_number`
    LIMIT 1
) AS `type1_image`,
(
    SELECT `types`.`image`
    FROM `types`
    INNER JOIN `pokemon_types` ON `pokemon_types`.`type_id` = `types`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_types`.`pokemon_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`id` = `pokemon`.`species_id`
    WHERE `pokemon_species`.`id` = `index_number`
    LIMIT 1,1
) AS `type2_image`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `hp`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1,1
) AS `atk`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 2,1
) AS `def`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 3,1
) AS `sp_atk`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 4,1
) AS `sp_def`,
(
    SELECT `pokemon_stats`.`base_stat`
    FROM `pokemon_stats`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_stats`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 5,1
) AS `spe`,
(
    SELECT `pokemon_color_names`.`name`
    FROM `pokemon_color_names`
    INNER JOIN `pokemon_colors` ON `pokemon_colors`.`id` = `pokemon_color_names`.`pokemon_color_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`color_id` = `pokemon_colors`.`id`
    WHERE `pokemon_species`.`id` = `index_number`
    AND `pokemon_color_names`.`local_language_id` = 9
    LIMIT 1
) AS `color`,
(
    SELECT `pokemon_shape_prose`.`name`
    FROM `pokemon_shape_prose`
    INNER JOIN `pokemon_shapes` ON `pokemon_shapes`.`id` = `pokemon_shape_prose`.`pokemon_shape_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`shape_id` = `pokemon_shapes`.`id`
    WHERE `pokemon_species`.`id` = `index_number`
    AND `pokemon_shape_prose`.`local_language_id` = 9
    LIMIT 1
) AS `shape`,
(
    SELECT `pokemon_habitat_names`.`name`
    FROM `pokemon_habitat_names`
    INNER JOIN `pokemon_habitats` ON `pokemon_habitats`.`id` = `pokemon_habitat_names`.`pokemon_habitat_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`habitat_id` = `pokemon_habitats`.`id`
    WHERE `pokemon_species`.`id` = `index_number`
    AND `pokemon_habitat_names`.`local_language_id` = 9
    LIMIT 1
) AS `habitat`,
`gender_rate`,
`capture_rate`,
`base_happiness`,
(
    SELECT `growth_rate_prose`.`name`
    FROM `growth_rate_prose`
    INNER JOIN `growth_rates` ON `growth_rates`.`id` = `growth_rate_prose`.`growth_rate_id`
    INNER JOIN `pokemon_species` ON `pokemon_species`.`growth_rate_id` = `growth_rates`.`id`
    WHERE `pokemon_species`.`id` = `index_number`
    AND `growth_rate_prose`.`local_language_id` = 9
    LIMIT 1
) AS `growth_rate`,
(
    SELECT `pokemon`.`height`
    FROM `pokemon`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `height`,
(
    SELECT `pokemon`.`weight`
    FROM `pokemon`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `weight`,
(
    SELECT `pokemon`.`base_experience`
    FROM `pokemon`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `base_experience`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_abilities`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1
) AS `ability1`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_abilities`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    AND `pokemon_abilities`.`is_hidden` = 0
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1,1
) AS `ability2`,
(
    SELECT `ability_names`.`name`
    FROM `abilities`
    INNER JOIN `pokemon_abilities` ON `pokemon_abilities`.`ability_id` = `abilities`.`id`
    INNER JOIN `ability_names` ON `ability_names`.`ability_id` = `abilities`.`id`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_abilities`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    AND `pokemon_abilities`.`is_hidden` = 1
    AND `ability_names`.`local_language_id` = 9
    LIMIT 1
) AS `hidden_ability`,
(
    SELECT `pokemon_species_flavor_text`.`flavor_text`
    FROM `pokemon_species_flavor_text`
    WHERE `pokemon_species_flavor_text`.`species_id` = `index_number`
    AND `pokemon_species_flavor_text`.`version_id` = 21
    AND `pokemon_species_flavor_text`.`language_id` = 9
    LIMIT 1
) AS `species_summary`,
(
    SELECT `pokemon_forms`.`icon`
    FROM `pokemon_forms`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_forms`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `icon`,
(
    SELECT `pokemon_forms`.`sprite`
    FROM `pokemon_forms`
    INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_forms`.`pokemon_id`
    WHERE `pokemon`.`species_id` = `index_number`
    LIMIT 1
) AS `sprite`
FROM `pokemon_species`
WHERE `pokemon_species`.`id` = @species_id;";
            }
        }

        public static string GetSpecificForm {
            get {
                return @"
SELECT `pokemon_forms`.`is_default`,
        `pokemon_forms`.`is_battle_only`,
        `pokemon_forms`.`is_mega`,
        `pokemon_forms`.`icon`,
        `pokemon_forms`.`sprite`,
        `pokemon_form_names`.`form_name`,
        `pokemon_form_names`.`pokemon_name`
FROM `pokemon_forms`
INNER JOIN `pokemon_form_names` ON `pokemon_form_names`.`pokemon_form_id` = `pokemon_forms`.`id`
INNER JOIN `pokemon` ON `pokemon`.`id` = `pokemon_forms`.`pokemon_id`
WHERE `pokemon_form_names`.`local_language_id` = 9
AND `pokemon`.`species_id` = @species_id;";
            }
        }
    }
}