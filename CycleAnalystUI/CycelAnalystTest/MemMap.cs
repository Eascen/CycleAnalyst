using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiscUtil.Conversion;

public class MemMap
{

    private byte[] valArray;
    EndianBitConverter converter = EndianBitConverter.Big;

    public MemMap(byte[] vals)
    {
        valArray = vals;
    }
    /// <summary>
    /// Trip Forward Ahrs
    /// </summary>
    public UInt32 PAhADR
    {
        get
        {
            return converter.ToUInt32(valArray, 0);
        }
    }
    /// <summary>
    /// Trip Regen Ahrs
    /// </summary>
    public UInt32 NAhADR
    {
        get
        {
            return converter.ToUInt32(valArray, 4);
        }
    }
    /// <summary>
    /// Trip Watt-hrs
    /// </summary>
    public UInt32 WAhADR
    {
        get
        {
            return converter.ToUInt32(valArray, 8);
        }
    }
    /// <summary>
    /// Trip Human Watt-hrs
    /// </summary>
    public UInt32 HWAhADR
    {
        get
        {
            return converter.ToUInt32(valArray, 12);
        }
    }
    /// <summary>
    /// Trip Max Current
    /// </summary>
    public UInt16 AMaxADR
    {
        get
        {
            return converter.ToUInt16(valArray, 16);
        }
    }
    /// <summary>
    /// Trip Min Current
    /// </summary>
    public UInt16 AMinADR
    {
        get
        {
            return converter.ToUInt16(valArray, 18);
        }
    }
    /// <summary>
    /// Trip Min Voltage
    /// </summary>
    public UInt16 VMinADR
    {
        get
        {
            return converter.ToUInt16(valArray, 20);
        }
    }
    /// <summary>
    /// Trip Distance
    /// </summary>
    public UInt32 DistADR
    {
        get
        {
            return converter.ToUInt32(valArray, 22);
        }
    }
    /// <summary>
    /// Trip Time with Speed > 0
    /// </summary>
    public UInt16 TimeADR
    {
        get
        {
            return converter.ToUInt16(valArray, 26);
        }
    }
    /// <summary>
    /// Trip Time with RPM > 0
    /// </summary>
    public UInt16 HTimeADR
    {
        get
        {
            return converter.ToUInt16(valArray, 28);
        }
    }
    /// <summary>
    /// Human Pedal Revolutions
    /// </summary>
    public UInt16 RevsADR
    {
        get
        {
            return converter.ToUInt16(valArray, 30);
        }
    }
    /// <summary>
    /// Maximum Speed
    /// </summary>
    public UInt16 SMaxADR
    {
        get
        {
            return converter.ToUInt16(valArray, 32);
        }
    }
    /// <summary>
    /// Speedo Number of Poles
    /// </summary>
    public UInt16 PolesADR
    {
        get
        {
            return converter.ToUInt16(valArray, 34);
        }
    }
    /// <summary>
    /// PAS Sensor Num. Poles
    /// </summary>
    public UInt16 PASPolesADR
    {
        get
        {
            return converter.ToUInt16(valArray, 36);
        }
    }
    /// <summary>
    /// Flag for Preferences
    /// </summary>
    public byte PrefFlagsADR
    {
        get
        {
            return valArray[38];
        }
    }
    /// <summary>
    /// Current Preset
    /// </summary>
    public byte PresetADR
    {
        get
        {
            return valArray[39];
        }
    }
    /// <summary>
    /// Number of Presets Enabled
    /// </summary>
    public byte PresetCntADR
    {
        get
        {
            return valArray[40];
        }
    }
    /// <summary>
    /// Name for Preset 1
    /// </summary>
    public byte Pre1NameADR
    {
        get
        {
            return valArray[41];
        }
    }
    /// <summary>
    /// Name for Preset 2
    /// </summary>
    public byte Pre2NameADR
    {
        get
        {
            return valArray[42];
        }
    }
    /// <summary>
    /// Name for Preset 3
    /// </summary>
    public byte Pre3NameADR
    {
        get
        {
            return valArray[43];
        }
    }
    /// <summary>
    /// Lifetime Odometer
    /// </summary>
    public UInt32 TotDistADR
    {
        get
        {
            return converter.ToUInt32(valArray, 44);
        }
    }
    /// <summary>
    /// Voltage Threshold for CA Shutdown
    /// </summary>
    public UInt16 VThreshADR
    {
        get
        {
            return converter.ToUInt16(valArray, 48);
        }
    }
    /// <summary>
    /// "Set Speed, Preset 1 "
    /// </summary>
    public UInt16 SetSpeedADR
    {
        get
        {
            return converter.ToUInt16(valArray, 50);
        }
    }
    /// <summary>
    /// "Set Speed, Preset 2"
    /// </summary>
    public UInt16 SetSpeedADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 52);
        }
    }
    /// <summary>
    /// "Set Speed, Preset 3"
    /// </summary>
    public UInt16 SetSpeedADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 54);
        }
    }
    /// <summary>
    /// "Threshold Speed, Preset 1"
    /// </summary>
    public UInt16 SetSpdMinADR
    {
        get
        {
            return converter.ToUInt16(valArray, 56);
        }
    }
    /// <summary>
    /// "Threshold Speed, Preset 2"
    /// </summary>
    public UInt16 SetSpdMinADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 58);
        }
    }
    /// <summary>
    /// "Threshold Speed, Preset 3"
    /// </summary>
    public UInt16 SetSpdMinADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 60);
        }
    }
    /// <summary>
    /// "Set Amps, Preset 1"
    /// </summary>
    public UInt16 SetAmpsADR
    {
        get
        {
            return converter.ToUInt16(valArray, 62);
        }
    }
    /// <summary>
    /// "Set Amps, Preset 2"
    /// </summary>
    public UInt16 SetAmpsADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 64);
        }
    }
    /// <summary>
    /// "Set Amps, Preset 3"
    /// </summary>
    public UInt16 SetAmpsADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 66);
        }
    }
    /// <summary>
    /// "Set Watts, Preset 1"
    /// </summary>
    public UInt16 SetWattsADR
    {
        get
        {
            return converter.ToUInt16(valArray, 68);
        }
    }
    /// <summary>
    /// "Set Watts, Preset 2"
    /// </summary>
    public UInt16 SetWattsADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 70);
        }
    }
    /// <summary>
    /// "Set Watts, Preset 3"
    /// </summary>
    public UInt16 SetWattsADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 72);
        }
    }
    /// <summary>
    /// Max Temperature Setpoint
    /// </summary>
    public UInt16 MaxTempADR
    {
        get
        {
            return converter.ToUInt16(valArray, 74);
        }
    }
    /// <summary>
    /// Threshold Temp Setpoint
    /// </summary>
    public UInt16 ThreshTempADR
    {
        get
        {
            return converter.ToUInt16(valArray, 76);
        }
    }
    /// <summary>
    /// Proportional Speed Feedback Gain
    /// </summary>
    public UInt16 PSGainADR
    {
        get
        {
            return converter.ToUInt16(valArray, 78);
        }
    }
    /// <summary>
    /// Integral Speed Feedback Gain
    /// </summary>
    public UInt16 IntSGainADR
    {
        get
        {
            return converter.ToUInt16(valArray, 80);
        }
    }
    /// <summary>
    /// Differential Speed Feedback Gain
    /// </summary>
    public UInt16 DSGainADR
    {
        get
        {
            return converter.ToUInt16(valArray, 82);
        }
    }
    /// <summary>
    /// Span for Differential Speed Calc 
    /// </summary>
    public UInt16 DGainSpanADR
    {
        get
        {
            return converter.ToUInt16(valArray, 84);
        }
    }
    /// <summary>
    /// Amps Limit Feedback Gain
    /// </summary>
    public UInt16 IntAGainADR
    {
        get
        {
            return converter.ToUInt16(valArray, 86);
        }
    }
    /// <summary>
    /// Low Voltage Feedback Gain
    /// </summary>
    public UInt16 IntVGainADR
    {
        get
        {
            return converter.ToUInt16(valArray, 88);
        }
    }
    /// <summary>
    /// Watts Limit Feedback Gain
    /// </summary>
    public UInt16 IntWGainADR
    {
        get
        {
            return converter.ToUInt16(valArray, 90);
        }
    }
    /// <summary>
    /// Max Throttle Output
    /// </summary>
    public UInt16 ThOMaxADR
    {
        get
        {
            return converter.ToUInt16(valArray, 92);
        }
    }
    /// <summary>
    /// Min Throttle Output
    /// </summary>
    public UInt16 ThOMinADR
    {
        get
        {
            return converter.ToUInt16(valArray, 94);
        }
    }
    /// <summary>
    /// Wheel Circumference
    /// </summary>
    public UInt16 WheelADR
    {
        get
        {
            return converter.ToUInt16(valArray, 96);
        }
    }
    /// <summary>
    /// Display Screen Mask
    /// </summary>
    public UInt16 DispStillADR
    {
        get
        {
            return converter.ToUInt16(valArray, 98);
        }
    }
    /// <summary>
    /// Moving Display Screen Mask
    /// </summary>
    public UInt16 DispMovADR
    {
        get
        {
            return converter.ToUInt16(valArray, 100);
        }
    }
    /// <summary>
    /// Display Averaging Period
    /// </summary>
    public UInt16 RefreshADR
    {
        get
        {
            return converter.ToUInt16(valArray, 102);
        }
    }
    /// <summary>
    /// Torque Sensor Enable
    /// </summary>
    public byte TrqEnabADR
    {
        get
        {
            return valArray[104];
        }
    }
    /// <summary>
    /// Torque Sensor Scale
    /// </summary>
    public UInt16 TrqScaleADR
    {
        get
        {
            return converter.ToUInt16(valArray, 105);
        }
    }
    /// <summary>
    /// Torque Sensor Offset Voltage
    /// </summary>
    public UInt16 TrqOSADR
    {
        get
        {
            return converter.ToUInt16(valArray, 107);
        }
    }
    /// <summary>
    /// Temperature Sensor Offset
    /// </summary>
    public UInt16 TOffsetADR
    {
        get
        {
            return converter.ToUInt16(valArray, 109);
        }
    }
    /// <summary>
    /// Temperature Sensor Scale
    /// </summary>
    public UInt16 TScaleADR
    {
        get
        {
            return converter.ToUInt16(valArray, 111);
        }
    }
    /// <summary>
    /// Threshold PAS Period for Start of Pedalling
    /// </summary>
    public UInt16 PASStartADR
    {
        get
        {
            return converter.ToUInt16(valArray, 113);
        }
    }
    /// <summary>
    /// Threshold Time for Pedal Stopping
    /// </summary>
    public UInt16 PASStopADR
    {
        get
        {
            return converter.ToUInt16(valArray, 115);
        }
    }
    /// <summary>
    /// Proportional Torque Assistance Level
    /// </summary>
    public UInt16 PASLevelADR
    {
        get
        {
            return converter.ToUInt16(valArray, 117);
        }
    }
    /// <summary>
    /// Proportional Torque Starting Point
    /// </summary>
    public UInt16 PASOSADR
    {
        get
        {
            return converter.ToUInt16(valArray, 119);
        }
    }
    /// <summary>
    /// Minimum Auxilliary Potentiometer Input
    /// </summary>
    public UInt16 AuxMinADR
    {
        get
        {
            return converter.ToUInt16(valArray, 121);
        }
    }
    /// <summary>
    /// Max Auxilliaryt Potentiometer Input
    /// </summary>
    public UInt16 AuxMaxADR
    {
        get
        {
            return converter.ToUInt16(valArray, 123);
        }
    }
    /// <summary>
    /// Min Throttle Input
    /// </summary>
    public UInt16 ThrotMinADR
    {
        get
        {
            return converter.ToUInt16(valArray, 125);
        }
    }
    /// <summary>
    /// Max Throttle Input
    /// </summary>
    public UInt16 ThrotMaxADR
    {
        get
        {
            return converter.ToUInt16(valArray, 127);
        }
    }
    /// <summary>
    /// Throttle Fault Voltage
    /// </summary>
    public UInt16 ThrotFaultADR
    {
        get
        {
            return converter.ToUInt16(valArray, 129);
        }
    }
    /// <summary>
    /// "Throttle Up Ramp, Preset 1"
    /// </summary>
    public UInt16 ThrotRateADR
    {
        get
        {
            return converter.ToUInt16(valArray, 131);
        }
    }
    /// <summary>
    /// "Throttle Up Ramp, Preset 2"
    /// </summary>
    public UInt16 ThrotRateADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 133);
        }
    }
    /// <summary>
    /// "Throttle Up Ramp, Preset 3"
    /// </summary>
    public UInt16 ThrotRateADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 135);
        }
    }
    /// <summary>
    /// Throttle Down Ramp
    /// </summary>
    public UInt16 ThrotDRateADR
    {
        get
        {
            return converter.ToUInt16(valArray, 137);
        }
    }
    /// <summary>
    /// "Input Throttle Mode, Preset 1"
    /// </summary>
    public byte ThrotModeADR
    {
        get
        {
            return valArray[139];
        }
    }
    /// <summary>
    /// "Input Throttle Mode, Preset 2"
    /// </summary>
    public byte ThrotModeADR2
    {
        get
        {
            return valArray[140];
        }
    }
    /// <summary>
    /// "Input Throttle Mode, Preset 3"
    /// </summary>
    public byte ThrotModeADR3
    {
        get
        {
            return valArray[141];
        }
    }
    /// <summary>
    /// "Aux Voltage Function, Preset 1"
    /// </summary>
    public byte AuxModeADR
    {
        get
        {
            return valArray[142];
        }
    }
    /// <summary>
    /// "Aux Voltage Function, Preset 2"
    /// </summary>
    public byte AuxModeADR2
    {
        get
        {
            return valArray[143];
        }
    }
    /// <summary>
    /// "Aux Voltage Function, Preset 3"
    /// </summary>
    public byte AuxModeADR3
    {
        get
        {
            return valArray[144];
        }
    }
    /// <summary>
    /// "PAS Mode, Preset 1"
    /// </summary>
    public byte PASModeADR
    {
        get
        {
            return valArray[145];
        }
    }
    /// <summary>
    /// "PAS Mode, Preset 2"
    /// </summary>
    public byte PASModeADR2
    {
        get
        {
            return valArray[146];
        }
    }
    /// <summary>
    /// "PAS Mode, Preset 3"
    /// </summary>
    public byte PASModeADR3
    {
        get
        {
            return valArray[147];
        }
    }
    /// <summary>
    /// Thermistor Mode Selection
    /// </summary>
    public byte ThermModeADR
    {
        get
        {
            return valArray[148];
        }
    }
    /// <summary>
    /// Battery Presets Enabled
    /// </summary>
    public byte PackCntADR
    {
        get
        {
            return valArray[149];
        }
    }
    /// <summary>
    /// Current Battery Preset
    /// </summary>
    public byte PackADR
    {
        get
        {
            return valArray[150];
        }
    }
    /// <summary>
    /// "Cell Count, Battery 1"
    /// </summary>
    public byte NumCellADR
    {
        get
        {
            return valArray[151];
        }
    }
    /// <summary>
    /// "Cell Count, Battery 2"
    /// </summary>
    public byte NumCellADR2
    {
        get
        {
            return valArray[152];
        }
    }
    /// <summary>
    /// "Cell Count, Battery 3"
    /// </summary>
    public byte NumCellADR3
    {
        get
        {
            return valArray[153];
        }
    }
    /// <summary>
    /// "Cell Chemistry, Battery 1"
    /// </summary>
    public byte CellChemADR
    {
        get
        {
            return valArray[154];
        }
    }
    /// <summary>
    /// "Cell Chemistry, Battery 2"
    /// </summary>
    public byte CellChemADR2
    {
        get
        {
            return valArray[155];
        }
    }
    /// <summary>
    /// "Cell Chemistry, Battery 3"
    /// </summary>
    public byte CellChemADR3
    {
        get
        {
            return valArray[156];
        }
    }
    /// <summary>
    /// "Pack Capacity, Battery 1"
    /// </summary>
    public UInt16 CellAhADR
    {
        get
        {
            return converter.ToUInt16(valArray, 157);
        }
    }
    /// <summary>
    /// "Pack Capacity, Battery 2"
    /// </summary>
    public UInt16 CellAhADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 159);
        }
    }
    /// <summary>
    /// "Pack Capacity, Battery 3"
    /// </summary>
    public UInt16 CellAhADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 161);
        }
    }
    /// <summary>
    /// "Life Cycle Amp-Hours, Battery 1"
    /// </summary>
    public UInt32 TAhADR
    {
        get
        {
            return converter.ToUInt32(valArray, 163);
        }
    }
    /// <summary>
    /// "Life Cycle Amp-Hours, Battery 2"
    /// </summary>
    public UInt32 TAhADR2
    {
        get
        {
            return converter.ToUInt32(valArray, 167);
        }
    }
    /// <summary>
    /// "Life Cycle Amp-Hours, Battery 3"
    /// </summary>
    public UInt32 TAhADR3
    {
        get
        {
            return converter.ToUInt32(valArray, 171);
        }
    }
    /// <summary>
    /// "Total Cycles, Battery 1"
    /// </summary>
    public UInt16 CyclesADR
    {
        get
        {
            return converter.ToUInt16(valArray, 175);
        }
    }
    /// <summary>
    /// "Total Cycles, Battery 2"
    /// </summary>
    public UInt16 CyclesADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 177);
        }
    }
    /// <summary>
    /// "Total Cycles, Battery 3"
    /// </summary>
    public UInt16 CyclesADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 179);
        }
    }
    /// <summary>
    /// "Internal Pack Resistance, Battery 1"
    /// </summary>
    public UInt16 RBattADR
    {
        get
        {
            return converter.ToUInt16(valArray, 181);
        }
    }
    /// <summary>
    /// "Internal Pack Resistance, Battery 2"
    /// </summary>
    public UInt16 RBattADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 183);
        }
    }
    /// <summary>
    /// "Internal Pack Resistance, Battery 3"
    /// </summary>
    public UInt16 RBattADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 185);
        }
    }
    /// <summary>
    /// "Low Voltage Cutoff, Battery 1"
    /// </summary>
    public UInt16 SetVoltsADR
    {
        get
        {
            return converter.ToUInt16(valArray, 187);
        }
    }
    /// <summary>
    /// "Low Voltage Cutoff, Battery 2"
    /// </summary>
    public UInt16 SetVoltsADR2
    {
        get
        {
            return converter.ToUInt16(valArray, 189);
        }
    }
    /// <summary>
    /// "Low Voltage Cutoff, Battery 3"
    /// </summary>
    public UInt16 SetVoltsADR3
    {
        get
        {
            return converter.ToUInt16(valArray, 191);
        }
    }
    /// <summary>
    /// Rate at which Rbatt is automatically calculated
    /// </summary>
    public UInt16 RBattRateADR
    {
        get
        {
            return converter.ToUInt16(valArray, 193);
        }
    }
    /// <summary>
    /// Smallest current to display (not implemented)
    /// </summary>
    public UInt16 DeltaAMinADR
    {
        get
        {
            return converter.ToUInt16(valArray, 195);
        }
    }
    /// <summary>
    /// Battery State of Charge on power-down 
    /// </summary>
    public UInt32 SOCADR
    {
        get
        {
            return converter.ToUInt32(valArray, 197);
        }
    }
    /// <summary>
    /// Mask for outer level setup menu (not implemented)
    /// </summary>
    public UInt16 OutrSetpMask
    {
        get
        {
            return converter.ToUInt16(valArray, 201);
        }
    }
    ///// <summary>
    ///// Mask for all inner setup menu items
    ///// </summary>
    //public UNKNOWN InnrSetupMask
    //{
    //    get
    //    {
    //    }
    //}
    /// <summary>
    /// Hard Limit for Max Speed Limit
    /// </summary>
    public UInt16 MaxSpeedADR
    {
        get
        {
            return converter.ToUInt16(valArray, 216);
        }
    }
    /// <summary>
    /// Hard Limit for Max Watts Limit
    /// </summary>
    public UInt16 MaxPowerADR
    {
        get
        {
            return converter.ToUInt16(valArray, 218);
        }
    }
    /// <summary>
    /// Hard Limit for Max Amps Limit
    /// </summary>
    public UInt16 MaxAmpsADR
    {
        get
        {
            return converter.ToUInt16(valArray, 220);
        }
    }
    /// <summary>
    /// Reserved for firmware version
    /// </summary>
    public UInt16 RESERVED_ADR
    {
        get
        {
            return converter.ToUInt16(valArray, 240);
        }
    }
    /// <summary>
    /// Voltage Offset of High Gain Amplifier
    /// </summary>
    public UInt16 OffsetHADR
    {
        get
        {
            return converter.ToUInt16(valArray, 242);
        }
    }
    /// <summary>
    /// Voltage Offset of Low Gain Amplifier
    /// </summary>
    public UInt16 OffsetLADR
    {
        get
        {
            return converter.ToUInt16(valArray, 244);
        }
    }
    /// <summary>
    /// Inverse of Rshunt (InvGain / Rshunt)
    /// </summary>
    public UInt16 AScaleADR
    {
        get
        {
            return converter.ToUInt16(valArray, 246);
        }
    }
    /// <summary>
    /// Voltage Calibration Scaling 
    /// </summary>
    public UInt16 VScaleADR
    {
        get
        {
            return converter.ToUInt16(valArray, 248);
        }
    }
    /// <summary>
    /// Factory Calibration of Op-Amp Gain
    /// </summary>
    public UInt32 InvGainADR
    {
        get
        {
            return converter.ToUInt32(valArray, 250);
        }
    }
    /// <summary>
    /// Ratio of High and Low Amplifier Gains
    /// </summary>
    public UInt16 RatioADR
    {
        get
        {
            return converter.ToUInt16(valArray, 254);
        }
    }
}
