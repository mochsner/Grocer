select R."Name", SS."Name", I."Name", RI."IngredientCount", I."Unit"  from "Recipe" R
                                                                               left join "RecipeIngredientJunction" RI
                                                                                         on RI."RecipeId" = R."Id"
                                                                               left join "Ingredient" I
                                                                                         on I."Id" = RI."IngredientId"
                                                                               left join "StoreSection" SS
                                                                                         on I."StoreSectionId" = SS."Id"
where R."Name" NOT LIKE 'Vegetable Soup'
order by SS."Name", I."Name"