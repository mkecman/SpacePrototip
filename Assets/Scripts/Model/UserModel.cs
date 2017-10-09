using UnityEngine;
using System.Collections;
using UniRx;

public class UserModel
{
    public JSONUserModel Model;

    public StringReactiveProperty rID = new StringReactiveProperty();
    public IntReactiveProperty rXP = new IntReactiveProperty();
    public FloatReactiveProperty rSC = new FloatReactiveProperty();
    public IntReactiveProperty rHC = new IntReactiveProperty();
    public IntReactiveProperty rStarsCreated = new IntReactiveProperty();
    public IntReactiveProperty rPlanetsCreated = new IntReactiveProperty();
    public ReactiveCollection<AtomModel> Atoms = new ReactiveCollection<AtomModel>();
    public ReactiveCollection<SolarModel> Galaxies = new ReactiveCollection<SolarModel>();

    public UserModel( JSONUserModel model )
    {
        Model = model;

        rID.Value = Model.ID;
        rXP.Value = Model.XP;
        rSC.Value = Model.SC;
        rHC.Value = Model.HC;
        rStarsCreated.Value = Model.StarsCreated;
        rPlanetsCreated.Value = Model.PlanetsCreated;

        int i;
        for( i = 0; i < model.Atoms.Count; i++ )
        {
            Atoms.Add( new AtomModel( Model.Atoms[ i ] ) );
        }

        for( i = 0; i < model.Galaxies.Count; i++ )
        {
            Galaxies.Add( new SolarModel( Model.Galaxies[ i ] ) );
        }
    }

    public string ID
    {
        set { Model.ID = value; rID.Value = value; }
        get { return rID.Value; }
    }

    public int XP
    {
        set { Model.XP = value; rXP.Value = value; }
        get { return rXP.Value; }
    }

    public float SC
    {
        set { Model.SC = value; rSC.Value = value; }
        get { return rSC.Value; }
    }

    public int HC
    {
        set { Model.HC = value; rHC.Value = value; }
        get { return rHC.Value; }
    }

    public int StarsCreated
    {
        set { Model.StarsCreated = value; rStarsCreated.Value = value; }
        get { return rStarsCreated.Value; }
    }

    public int PlanetsCreated
    {
        set { Model.PlanetsCreated = value; rPlanetsCreated.Value = value; }
        get { return rPlanetsCreated.Value; }
    }
}
