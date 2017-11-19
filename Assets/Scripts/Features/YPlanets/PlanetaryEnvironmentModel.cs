using UnityEngine;
using UniRx;
using System;

public class PlanetaryEnvironmentModel
{
    public StringReactiveProperty rName = new StringReactiveProperty();
    public StringReactiveProperty rSymbol = new StringReactiveProperty();
    
    public FloatReactiveProperty rCurrentValue = new FloatReactiveProperty();
    public FloatReactiveProperty rMinValue = new FloatReactiveProperty();
    public FloatReactiveProperty rMedValue = new FloatReactiveProperty();
    public FloatReactiveProperty rMaxValue = new FloatReactiveProperty();

    public FloatReactiveProperty rStrength = new FloatReactiveProperty();

    public PlanetaryEnvironmentModel(JSONPlanetaryEnvironmentModel model)
    {
        fromJSON(model);
        rCurrentValue.Subscribe( _ => UpdateStrength() );
    }

    private void UpdateStrength()
    {
        if( CurrentValue >= MedValue )
        {
            rStrength.Value = ( ( CurrentValue - MedValue ) / ( MaxValue - MedValue ) );
        }
        else
        {
            rStrength.Value = ( ( CurrentValue - MedValue ) / ( MinValue - MedValue ) );
        }
    }

    public PlanetaryEnvironmentModel Copy()
    {
        return new PlanetaryEnvironmentModel(this.toJSON());
    }

    public void fromJSON(JSONPlanetaryEnvironmentModel model)
    {
        rName.Value = model.Name;
        rSymbol.Value = model.Symbol;
        rCurrentValue.Value = model.CurrentValue;
        rMinValue.Value = model.MinValue;
        rMedValue.Value = model.MedValue;
        rMaxValue.Value = model.MaxValue;
        rStrength.Value = model.Strength;
    }

    public JSONPlanetaryEnvironmentModel toJSON()
    {
        JSONPlanetaryEnvironmentModel model = new JSONPlanetaryEnvironmentModel();

        model.Name = rName.Value;
        model.Symbol = rSymbol.Value;
        model.CurrentValue = rCurrentValue.Value;
        model.MinValue = rMinValue.Value;
        model.MedValue = rMedValue.Value;
        model.MaxValue = rMaxValue.Value;
        model.Strength = rStrength.Value;

        return model;
    }
    
    public string Name
    {
        set { rName.Value = value; }
        get { return rName.Value; }
    }

    public string Symbol
    {
        set { rSymbol.Value = value; }
        get { return rSymbol.Value; }
    }

    public float CurrentValue
    {
        set { rCurrentValue.Value = value; }
        get { return rCurrentValue.Value; }
    }

    public float MinValue
    {
        set { rMinValue.Value = value; }
        get { return rMinValue.Value; }
    }

    public float MedValue
    {
        set { rMedValue.Value = value; }
        get { return rMedValue.Value; }
    }

    public float MaxValue
    {
        set { rMaxValue.Value = value; }
        get { return rMaxValue.Value; }
    }

    public float Strength
    {
        get { return rStrength.Value; }
    }

}
