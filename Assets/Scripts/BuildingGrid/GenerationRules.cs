using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GenerationRules
{
    public List<ProdecuralGenerator.RoadTypes> ups, downs, rights, lefts;

    public GenerationRules(
        List<ProdecuralGenerator.RoadTypes> up, 
        List<ProdecuralGenerator.RoadTypes> down, 
        List<ProdecuralGenerator.RoadTypes> right, 
        List<ProdecuralGenerator.RoadTypes> left
    ) {
        this.ups = up;
        this.downs = down;
        this.rights = right;
        this.lefts = left;
    }
}
