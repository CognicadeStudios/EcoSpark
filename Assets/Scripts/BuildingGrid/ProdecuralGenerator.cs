using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdecuralGenerator : MonoBehaviour
{
    public List<GenerationRules> prefabsData;
    public List<GameObject> prefabsList;
    public enum RoadTypes {
        Crossroad,
        Empty,
        StraightX,
        StraightY,
        RoadNE,
        RoadNW,
        RoadSE,
        RoadSW
    };

    public void Awake()
    {
        prefabsData = new List<GenerationRules>();

        //Crossroads can be surrounded by every type of road, and other crossroads
        prefabsData.Add(new GenerationRules(
            new List<RoadTypes> { RoadTypes.RoadNW, RoadTypes.RoadSW, RoadTypes.Crossroad, RoadTypes.StraightY},
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadSE, RoadTypes.Crossroad, RoadTypes.StraightY},
            new List<RoadTypes> { RoadTypes.RoadNE, RoadTypes.RoadNW, RoadTypes.Crossroad, RoadTypes.StraightX},
            new List<RoadTypes> { RoadTypes.RoadSE, RoadTypes.RoadSW, RoadTypes.Crossroad, RoadTypes.StraightX}
        ));

        //Empty Tiles can be surrounded by other empty tiles, turns, and sides of roads
        prefabsData.Add(new GenerationRules(
            new List<RoadTypes> {RoadTypes.RoadNE, RoadTypes.RoadSE, RoadTypes.StraightX, RoadTypes.Empty},
            new List<RoadTypes> {RoadTypes.RoadNW, RoadTypes.RoadSW, RoadTypes.StraightX, RoadTypes.Empty},
            new List<RoadTypes> {RoadTypes.RoadSE, RoadTypes.RoadSW, RoadTypes.StraightY, RoadTypes.Empty},
            new List<RoadTypes> {RoadTypes.RoadNE, RoadTypes.RoadNW, RoadTypes.StraightY, RoadTypes.Empty}
        ));

        //StraightX
        prefabsData.Add(new GenerationRules(
            new List<RoadTypes> { RoadTypes.Empty, RoadTypes.StraightX, RoadTypes.RoadNE, RoadTypes.RoadSE},
            new List<RoadTypes> { RoadTypes.Empty, RoadTypes.StraightX, RoadTypes.RoadNW, RoadTypes.RoadSW},
            new List<RoadTypes> { RoadTypes.Crossroad, RoadTypes.StraightX, RoadTypes.RoadNW, RoadTypes.RoadNE},
            new List<RoadTypes> { RoadTypes.Crossroad, RoadTypes.StraightX, RoadTypes.RoadSW, RoadTypes.RoadSE}
        ));
    }
}
