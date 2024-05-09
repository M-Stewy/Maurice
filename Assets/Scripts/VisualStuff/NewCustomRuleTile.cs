using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
/// <summary>
/// Made by Stewy
/// 
/// this is just a special thing to allow us to make better rule tiles
/// This special type of rule tile is only to make it so it can detect other tiles that arnt itself
/// This is pretty much copied verbatim from a video by Vinark117 
/// https://www.youtube.com/watch?v=FwOxLkJTXag&list=PL3mmtPuKm3XhpaPVABjmUH68a_wd2hDcr&index=14
/// </summary>
[CreateAssetMenu(menuName = "CustomTile/AdvancedRuleTileTest")]
public class NewCustomRuleTile : RuleTile<NewCustomRuleTile.Neighbor> {
    public TileBase[] tilesToConnect;

    public bool alwaysConnect;
    public bool CheckSelf;


    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Any = 3;
        public const int Specified = 4;
        public const int Nothing = 5;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.This: return Check_This(tile);
            case Neighbor.NotThis: return Check_NotThis(tile);
            case Neighbor.Any: return Check_Any(tile);
            case Neighbor.Specified: return Check_Specified(tile);
            case Neighbor.Nothing: return Check_Nothing(tile);

        }
        return base.RuleMatch(neighbor, tile);
    }

    bool Check_This(TileBase tile)
    {
        if (!alwaysConnect) return tile = this;
        else return  tilesToConnect.Contains(this) || tile == this;
    }

    bool Check_NotThis(TileBase tile)
    {
        return tile != this;
    }

    bool Check_Any(TileBase tile)
    {
        if (CheckSelf) return tile != null;
        else return tile != null && tile != this;
    }

    bool Check_Specified(TileBase tile)
    {
        return tilesToConnect.Contains(tile);
    }

    bool Check_Nothing(TileBase tile)
    {
        return tile == null;
    }
}