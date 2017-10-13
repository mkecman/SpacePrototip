using System.Collections.Generic;
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

    public JSONUserModel toJSON()
    {
        JSONUserModel model = new JSONUserModel();

        model.ID = rID.Value;
        model.XP = rXP.Value;
        model.SC = rSC.Value;
        model.HC = rHC.Value;
        model.StarsCreated = rStarsCreated.Value;
        model.PlanetsCreated = rPlanetsCreated.Value;

        model.Atoms = new List<JSONAtomModel>();
        int i;
        for( i = 0; i < Atoms.Count; i++ )
        {
            model.Atoms.Add( Atoms[ i ].toJSON() );
        }

        model.Galaxies = new List<JSONSolarModel>();
        for( i = 0; i < Galaxies.Count; i++ )
        {
            model.Galaxies.Add( Galaxies[ i ].toJSON() );
        }

        return model;
    }

    public string ID
    {
        set { rID.Value = value; }
        get { return rID.Value; }
    }

    public int XP
    {
        set { rXP.Value = value; }
        get { return rXP.Value; }
    }

    public float SC
    {
        set { rSC.Value = value; }
        get { return rSC.Value; }
    }

    public int HC
    {
        set { rHC.Value = value; }
        get { return rHC.Value; }
    }

    public int StarsCreated
    {
        set { rStarsCreated.Value = value; }
        get { return rStarsCreated.Value; }
    }

    public int PlanetsCreated
    {
        set { rPlanetsCreated.Value = value; }
        get { return rPlanetsCreated.Value; }
    }
}
