using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PokemonStats_WPF {
    public class Data {
        private DataSet pokedexDataSet = null;
        private DataView pokedexDataView = null;

        public DataView PokedexDataView {
            get { return pokedexDataView; }
        }

        public DataSet PokedexDataSet {
            get { return pokedexDataSet; }
        }

        public Data() {
            pokedexDataSet = new DataSet("pokedex");
            pokedexDataView = LoadPokedex();
        }

        //private DataView CreatePokedex() {
        //    DataTable pokemonTable = new DataTable("pokemon");
        //    DataColumn column;
        //    DataRow row;

        //    #region DataTable (pokemon) columns
        //    column = new DataColumn();
        //    column.DataType = Type.GetType("System.String");
        //    column.ColumnName = "name";
        //    pokemonTable.Columns.Add(column);
        //    #endregion

        //    pokedexDataSet.Tables.Add(pokemonTable);

        //    for(int i = 0; i < 668; i++) {
        //        row = pokemonTable.NewRow();
        //        row["name"] = pokemonList[i];
        //        pokemonTable.Rows.Add(row);
        //    }

        //    return pokemonTable.DefaultView;
        //}

        //[Obsolete("Used only with a database", true)]
        public DataView LoadPokedex() {
            DataTable pokedex = null;
            using(MySqlConnection connection = new MySqlConnection("Server=localhost;Database=pkmn;Uid=root;Pwd=compaq;SslMode=None")) {
                connection.Open();
                string q = $@"
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
) AS `hidden_ability`
FROM `pokemon`
WHERE `pokemon`.`id` <= 721;
";
                using(MySqlDataAdapter adapter = new MySqlDataAdapter(q, connection)) {
                    adapter.Fill(pokedexDataSet, "pokedex");
                    pokedex = pokedexDataSet.Tables["pokedex"];
                }
                connection.Close();
            }

            pokedex.Columns["pokemon_id"].ColumnName = "No";
            pokedex.Columns["name"].ColumnName = "Pokemon";
            pokedex.Columns["color"].ColumnName = "Color";
            pokedex.Columns["shape"].ColumnName = "Shape";
            pokedex.Columns["habitat"].ColumnName = "Habitat";
            pokedex.Columns["gender_rate"].ColumnName = "Gender rate";
            pokedex.Columns["capture_rate"].ColumnName = "Capture rate";
            pokedex.Columns["base_happiness"].ColumnName = "Base hapiness";
            pokedex.Columns["growth_rate"].ColumnName = "Growth rate";
            pokedex.Columns["height"].ColumnName = "Height";
            pokedex.Columns["weight"].ColumnName = "Weight";
            pokedex.Columns["base_experience"].ColumnName = "Base experience";
            pokedex.Columns["ability1"].ColumnName = "Ability 1";
            pokedex.Columns["ability2"].ColumnName = "Ability 2";
            pokedex.Columns["hidden_ability"].ColumnName = "Hidden Ability";

            return pokedex.DefaultView;
        }
    }
}
