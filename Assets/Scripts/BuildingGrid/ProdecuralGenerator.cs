using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;
public class ProdecuralGenerator : MonoBehaviour
{
    public List<GenerationRules> prefabsData;
    public enum RoadTypes {
        Empty,
        Crossroad,
        StraightX,
        StraightY,
        RoadNE,
        RoadNW,
        RoadSE,
        RoadSW,
    };

    public List<RoadTypes>[,] entropies;
    public bool[,] built;
    public int n;
    public float[] probabilities;    
    public int randomSeed;
    public Random random;
    public List<int> goodSeeds;
    /// <summary>
    /// Given a list of road types, randomly selects one of the types
    /// using the probabilities given in the probabilities array.
    /// </summary>
    /// <param name="entropy">A list of road types to randomly select from</param>
    /// <returns>The randomly selected road type</returns>
    public RoadTypes GetRandomRoad(List<RoadTypes> entropy)
    {
        float totalProb = 0.0f;
        foreach(RoadTypes r in entropy) 
        {
            totalProb += probabilities[(int)r];
        }

        float randVal = (float)random.NextDouble() * totalProb;
        float cur = 0;
        foreach(RoadTypes r in entropy) 
        {
            cur += probabilities[(int)r];
            if(cur >= randVal)
            {
                return r;
            }
        }

        return entropy[0];
    }

        /// <summary>
        /// Initializes the Prodecural Generator with the given grid size n
        /// </summary>
        /// <param name="n">The size of the grid to generate</param>
        /// <remarks>
        /// After calling this method, the Prodecural Generator is ready to generate
        /// roads. To generate roads, simply call the GenerateRoads method.
        /// </remarks>
    public void Initialize(int n)
    {
        //this.randomSeed = goodSeeds[UnityEngine.Random.Range(0, goodSeeds.Count - 1)];
        this.random = new Random(randomSeed);
        this.probabilities = new float[]
        {
            1.5f,
            0.3f,
            1.0f,
            0.3f,
            0.03f,
            0.03f,
            0.03f,
            0.03f
        };

        this.n = n;
        prefabsData = new List<GenerationRules>{
            //Empty Tiles can be surrounded by other empty tiles, turns, and sides of roads
            new GenerationRules(
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadSE, RoadTypes.StraightX, RoadTypes.Empty },
            new List<RoadTypes> { RoadTypes.RoadNW, RoadTypes.RoadSW, RoadTypes.StraightX, RoadTypes.Empty },
            new List<RoadTypes> { RoadTypes.RoadSE, RoadTypes.RoadSW, RoadTypes.StraightY, RoadTypes.Empty },
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadNW, RoadTypes.StraightY, RoadTypes.Empty }
        ),
            //Crossroads can be surrounded by every type of road, and other crossroads
            new GenerationRules(
            new List<RoadTypes> { RoadTypes.RoadNW, RoadTypes.RoadSW, RoadTypes.Crossroad, RoadTypes.StraightY },
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadSE, RoadTypes.Crossroad, RoadTypes.StraightY },
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadNW, RoadTypes.Crossroad, RoadTypes.StraightX },
            new List<RoadTypes> { RoadTypes.RoadSE, RoadTypes.RoadSW, RoadTypes.Crossroad, RoadTypes.StraightX }
        ),

            //StraightX
            new GenerationRules(
            new List<RoadTypes> { RoadTypes.Empty, RoadTypes.StraightX, RoadTypes.RoadNE, RoadTypes.RoadSE },
            new List<RoadTypes> { RoadTypes.Empty, RoadTypes.StraightX, RoadTypes.RoadNW, RoadTypes.RoadSW },
            new List<RoadTypes> { RoadTypes.Crossroad, RoadTypes.StraightX, RoadTypes.RoadNW, RoadTypes.RoadNE },
            new List<RoadTypes> { RoadTypes.Crossroad, RoadTypes.StraightX, RoadTypes.RoadSW, RoadTypes.RoadSE }
        ),

            //StraightY
            new GenerationRules(
            new List<RoadTypes> { RoadTypes.StraightY, RoadTypes.Crossroad, RoadTypes.RoadSW, RoadTypes.RoadNW },
            new List<RoadTypes> { RoadTypes.StraightY, RoadTypes.Crossroad, RoadTypes.RoadSE, RoadTypes.RoadNE },
            new List<RoadTypes> { RoadTypes.StraightY, RoadTypes.Empty, RoadTypes.RoadSE, RoadTypes.RoadSW },
            new List<RoadTypes> { RoadTypes.StraightY, RoadTypes.Empty, RoadTypes.RoadNW, RoadTypes.RoadNE }
        ),

            //RoadNE
            new GenerationRules(
            new List<RoadTypes> { RoadTypes.RoadNW, RoadTypes.RoadSW, RoadTypes.StraightY, RoadTypes.Crossroad },
            new List<RoadTypes> { RoadTypes.Empty, RoadTypes.StraightX, RoadTypes.RoadSW, RoadTypes.RoadNW },
            new List<RoadTypes> { RoadTypes.RoadSE, RoadTypes.RoadSW, RoadTypes.Empty, RoadTypes.StraightY },
            new List<RoadTypes> { RoadTypes.RoadSE, RoadTypes.RoadSW, RoadTypes.StraightX, RoadTypes.Crossroad }
        ),


            //RoadNW
            new GenerationRules(
            new List<RoadTypes> { RoadTypes.Empty, RoadTypes.StraightX, RoadTypes.RoadSE, RoadTypes.RoadNE },
            new List<RoadTypes> { RoadTypes.Crossroad, RoadTypes.StraightY, RoadTypes.RoadSE, RoadTypes.RoadNE },
            new List<RoadTypes> { RoadTypes.RoadSE, RoadTypes.RoadSW, RoadTypes.Empty, RoadTypes.StraightY },
            new List<RoadTypes> { RoadTypes.RoadSE, RoadTypes.RoadSW, RoadTypes.StraightX, RoadTypes.Crossroad }
        ),

            //RoadSE
            new GenerationRules(
            new List<RoadTypes> { RoadTypes.RoadNW, RoadTypes.RoadSW, RoadTypes.StraightY, RoadTypes.Crossroad },
            new List<RoadTypes> { RoadTypes.Empty, RoadTypes.StraightX, RoadTypes.RoadSW, RoadTypes.RoadNW },
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadNW, RoadTypes.StraightX, RoadTypes.Crossroad },
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadNW, RoadTypes.Empty, RoadTypes.StraightY }
        ),

            //RoadSW
            new GenerationRules(
            new List<RoadTypes> { RoadTypes.Empty, RoadTypes.StraightX, RoadTypes.RoadSE, RoadTypes.RoadNE },
            new List<RoadTypes> { RoadTypes.Crossroad, RoadTypes.StraightY, RoadTypes.RoadSE, RoadTypes.RoadNE },
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadNW, RoadTypes.StraightX, RoadTypes.Crossroad },
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadNW, RoadTypes.Empty, RoadTypes.StraightY }
        )
        };

        //generates a n*n grid

        entropies = new List<RoadTypes>[n, n];
        built = new bool[n, n];

        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                built[i, j] = false;
                entropies[i, j] = new List<RoadTypes> {
                    RoadTypes.Crossroad,
                    RoadTypes.Empty,
                    RoadTypes.StraightX,
                    RoadTypes.StraightY,
                    RoadTypes.RoadNE,
                    RoadTypes.RoadNW,
                    RoadTypes.RoadSE,
                    RoadTypes.RoadSW
                };
            }
        }
    }

        /// <summary>
        /// Generates the next tile in the grid by finding the tile with the most entropy, randomly selecting a roadtype from that tile, and then updating the entropies of the surrounding tiles.
        /// </summary>
        /// <returns>True if a tile was successfully generated, false if all tiles have been generated.</returns>
    public bool GenerateNextTile()
    {
        int foundR = -1, foundC = -1;
        for(int i = 0; i < n; i++) 
        {
            for(int j = 0; j < n; j++)
            {
                if((foundR == -1 || entropies[i, j].Count < entropies[foundC, foundR].Count) && !built[i, j])
                {
                    foundR = i;
                    foundC = j;
                }
            }
        }

        //All entropies are 1
        if(foundR == -1) return false;

        //For the current found tile, randomly pick a roadtype and place it
        RoadTypes randomTile = GetRandomRoad(entropies[foundR, foundC]);
        if(entropies[foundR, foundC].Contains(RoadTypes.Empty) && random.Next(0, 10) > 5)
        {
            randomTile = RoadTypes.Empty;
        }

        /*
        Debug.Log(foundR + ", " + foundC);
        String l = "";
        foreach(var v in entropies[foundR, foundC])
        {
            l += (int)v + ", ";
        }
        Debug.Log(l);
        */

        int tileInd = (int)(randomTile);
        //print(tileInd);
        //Update the entropies of the surrounding tiles
        int[] rDirs = {1, -1, 0, 0};
        int[] cDirs = {0, 0, 1, -1};
        for(int k = 0; k < 4; k++)
        {
            int nr = foundR + rDirs[k], nc = foundC + cDirs[k];
            if(nr < 0 || nc < 0 || nr >= n || nc >= n || built[nr, nc])
            {
                continue;
            }
            for(int i = entropies[nr, nc].Count - 1; i >= 0; i--)
            {
                switch(k)
                {
                    case 0:
                        //up
                        if(!prefabsData[tileInd].rights.Contains(entropies[nr, nc][i]))
                        {
                            entropies[nr, nc].RemoveAt(i);
                        }
                        break;
                    case 1:
                        //down
                        if(!prefabsData[tileInd].lefts.Contains(entropies[nr, nc][i]))
                        {
                            entropies[nr, nc].RemoveAt(i);
                        }
                        break;
                    case 2:
                        //right
                        if(!prefabsData[tileInd].ups.Contains(entropies[nr, nc][i]))
                        {
                            entropies[nr, nc].RemoveAt(i);
                        }
                        break;
                    case 3:
                        //left
                        if(!prefabsData[tileInd].downs.Contains(entropies[nr, nc][i]))
                        {
                            entropies[nr, nc].RemoveAt(i);
                        }
                        break;
                }
            }
        }

        //Spawn in the tile in world space :)
        GameObject building = GridController.instance.SetBuilding(foundR, foundC, (BuildingType)(tileInd));
        BuildingController controller = building.transform.parent.GetComponent<BuildingController>();
        controller.isBuildingMode = false;
        built[foundR, foundC] = true;

        return true;
    }
}
