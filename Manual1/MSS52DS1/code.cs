<?xml version="1.0" encoding="utf-8"?>
<fragment xmlns="http://www.holeschak.de/BmwDeepObd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.holeschak.de/BmwDeepObd ../../BmwDeepObd.xsd">
<code show_warnings="true">
<![CDATA[
class PageClass {
	public static Android.Graphics.Color val2Rgb(double value, double max, double offset) {
		if (value < 0) value = 0;

		// Add offset or RGB calculations so min is 0 and max is (max + offset)
		max   += offset;
		value += offset;

		if (value > max) value = max;

		// RGB multiplier calculation
		value = value * (255 / max);

		double value1 = value * 2;
		double value2 = 255 - value1;

		if (value >= 255.0) { return Android.Graphics.Color.Rgb(255,   0,   0); }
		if (value == 127.5) { return Android.Graphics.Color.Rgb(0,   255,   0); }
		if (value <=   0.0) { return Android.Graphics.Color.Rgb(0,     0, 255); }

		if (value < 127.5) {
			return Android.Graphics.Color.Rgb(0, (int) value1, (int) value2);
		}

		return Android.Graphics.Color.Rgb((int) value1, (int) value2, 0);
	}


	public string FormatResult(JobReader.PageInfo pageInfo, MultiMap<string, EdiabasNet.ResultData> resultDict, string resultName, ref Android.Graphics.Color? textColor, ref double? dataValue) {
		bool found;
		double value;
		string result = string.Empty;

		// Adjustments/offsets
		double ambient_offset = 1000; // Unit: hPa

		// Conversion multipliers
		double km2mi   =  0.621371192;
		double bar2psi = 14.503773773;
		double hpa2psi =  0.014503773;


		switch (resultName) {
			case "STATUS_TZ1#STAT_TZ1_WERT":
			case "STATUS_TZ2#STAT_TZ2_WERT":
			case "STATUS_TZ3#STAT_TZ3_WERT":
			case "STATUS_TZ4#STAT_TZ4_WERT":
			case "STATUS_TZ5#STAT_TZ5_WERT":
			case "STATUS_TZ6#STAT_TZ6_WERT":
			case "STATUS_TZ7#STAT_TZ7_WERT":
			case "STATUS_TZ8#STAT_TZ8_WERT": // Ignition timing
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				if (value > 64) value = value - 6553.6;
				dataValue = (double) value;

				result    = string.Format(ActivityMain.Culture, "{0,4:0.0}", value);
				textColor = val2Rgb(value, 50, 20);
				break;


			case "STATUS_AQREL#STAT_AQREL_WERT": // Throttle body actual cross section
			case "STATUS_RF#STAT_RF_WERT": // RF %
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result    = string.Format(ActivityMain.Culture, "{0,3:0.0}", value);
				textColor = val2Rgb(value, 120, 0);
				break;


			case "STATUS_L_SONDE#STAT_L_SONDE_WERT":
			case "STATUS_L_SONDE_2#STAT_L_SONDE_2_WERT": // Lambda sensor voltage
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result    = string.Format(ActivityMain.Culture, "{0,5:0.000}", value);
				textColor = val2Rgb(value, 1.2, 0);
				break;

			case "STATUS_ADD#STAT_ADD_WERT":
			case "STATUS_ADD_2#STAT_ADD_2_WERT": // Lambda additive
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				if (value > 64) value = value - 131.072;
				dataValue = (double) value;

				result    = string.Format(ActivityMain.Culture, "{0,5:0.000}", value);
				textColor = val2Rgb(value, 10, 10);
				break;

			case "STATUS_INT#STAT_INT_WERT":
			case "STATUS_INT_2#STAT_INT_2_WERT": // Lambda integral
			case "STATUS_LAMBDA_MUL_1#STAT_LAMBDA_MUL_1_WERT":
			case "STATUS_LAMBDA_MUL_2#STAT_LAMBDA_MUL_2_WERT": // Lambda multiplicative
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result    = string.Format(ActivityMain.Culture, "{0,5:0.000}", value);
				textColor = val2Rgb(value, 1.25, 0.25);
				break;


			case "STATUS_KUEHLW_AUSL_TEMPERATUR#STAT_KUEHLW_AUSL_TEMPERATUR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_CEngDsT_tSens_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_MOTOROEL_TEMPERATUR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_MOTORTEMPERATUR_WERT": // Radiator outlet/coolant/oil/refrigerant temp
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result    = string.Format(ActivityMain.Culture, "{0,3:0}", value);
				textColor = val2Rgb(value, 100, 10);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_KRAFTSTOFFTEMPERATUR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_UMGEBUNGSTEMPERATUR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADELUFTTEMPERATUR_WERT": // Ambient air/intake air/fuel temp
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result    = string.Format(ActivityMain.Culture, "{0,3:0}", value);
				textColor = val2Rgb(value, 40, 10);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_ABGASTEMPERATUR_VOR_KATALYSATOR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_ABGASTEMPERATUR_VOR_PARTIKELFILTER_1_WERT": // Exhaust temp
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result    = string.Format(ActivityMain.Culture, "{0,3:0}", value);
				textColor = val2Rgb(value, 800, 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_AFS_dmSens_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LUFTMASSE_PRO_HUB_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LUFTMASSE_SOLL_WERT": // Air mass
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result    = string.Format(ActivityMain.Culture, "{0,4:0}", value);
				textColor = val2Rgb(value, 1200, 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_RAILDRUCK_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_RAILDRUCK_SOLL_WERT": // Fuel rail pressure
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				value = value * bar2psi;
				dataValue = (double) value;

				result    = string.Format(ActivityMain.Culture, "{0,4:0}", value);
				textColor = val2Rgb(value, 400, 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_SOLL_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_DIFFERENZDRUCK_UEBER_PARTIKELFILTER_WERT": // Boost pressure/exhaust back pressure
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				value = (value - ambient_offset) * hpa2psi;
				dataValue = (double) value;

				result    = string.Format(ActivityMain.Culture, "{0,5:0.00}", value);
				textColor = val2Rgb(value, 40, 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_UBATT2_WERT": // Battery voltage
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				value = value / 1000;
				dataValue = (double) value;

				result    = string.Format(ActivityMain.Culture, "{0,5:0.00}", value);
				textColor = val2Rgb(value, 16, 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_STRECKE_SEIT_ERFOLGREICHER_REGENERATION_WERT": // DPF distance since regeneration
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				// Convert km to mi
				value = (value / 1000) * km2mi;
				dataValue = (double) value;

				result = string.Format(ActivityMain.Culture, "{0,6:0.0}", value);
				break;


			case "STATUS_MESSWERTBLOCK_LESEN#STAT_OELDRUCKSCHALTER_EIN_WERT": // Oil pressure switch
			case "STATUS_DIGITAL#STAT_AC_EIN":
			case "STATUS_DIGITAL#STAT_AKL_EIN":
			case "STATUS_DIGITAL#STAT_BA_EIN":
			case "STATUS_DIGITAL#STAT_BLS_EIN":
			case "STATUS_DIGITAL#STAT_BLTS_EIN":
			case "STATUS_DIGITAL#STAT_EKP_EIN":
			case "STATUS_DIGITAL#STAT_ELU_EIN":
			case "STATUS_DIGITAL#STAT_EWS3_FREIGABE":
			case "STATUS_DIGITAL#STAT_FDYN_EIN":
			case "STATUS_DIGITAL#STAT_FGRA_EIN":
			case "STATUS_DIGITAL#STAT_KATHEIZEN_EIN":
			case "STATUS_DIGITAL#STAT_KL15_EIN":
			case "STATUS_DIGITAL#STAT_KL50_EIN":
			case "STATUS_DIGITAL#STAT_KLOPFREGELUNG_EIN":
			case "STATUS_DIGITAL#STAT_KLOPFREG_EIN":
			case "STATUS_DIGITAL#STAT_KO_EIN":
			case "STATUS_DIGITAL#STAT_KRAFTSCHLUSS_EIN":
			case "STATUS_DIGITAL#STAT_KUP_EIN":
			case "STATUS_DIGITAL#STAT_LDP_EIN":
			case "STATUS_DIGITAL#STAT_LDR1_EIN":
			case "STATUS_DIGITAL#STAT_LDR2_EIN":
			case "STATUS_DIGITAL#STAT_LLR_EIN":
			case "STATUS_DIGITAL#STAT_LL_EIN":
			case "STATUS_DIGITAL#STAT_LSHN1_EIN":
			case "STATUS_DIGITAL#STAT_LSHN2_EIN":
			case "STATUS_DIGITAL#STAT_LSHV1_EIN":
			case "STATUS_DIGITAL#STAT_LSHV2_EIN":
			case "STATUS_DIGITAL#STAT_MFL1_EIN":
			case "STATUS_DIGITAL#STAT_MFL2_EIN":
			case "STATUS_DIGITAL#STAT_MFL3_EIN":
			case "STATUS_DIGITAL#STAT_MFL4_EIN":
			case "STATUS_DIGITAL#STAT_MIL_EIN":
			case "STATUS_DIGITAL#STAT_MOTOR_NACHLAUF":
			case "STATUS_DIGITAL#STAT_MOTOR_START":
			case "STATUS_DIGITAL#STAT_MOTOR_STEHT":
			case "STATUS_DIGITAL#STAT_NOISE_EIN":
			case "STATUS_DIGITAL#STAT_REEDSWITCH_EIN":
			case "STATUS_DIGITAL#STAT_SA_EIN":
			case "STATUS_DIGITAL#STAT_SCHUTZ_EIN":
			case "STATUS_DIGITAL#STAT_SLP_EIN":
			case "STATUS_DIGITAL#STAT_SLV_EIN":
			case "STATUS_DIGITAL#STAT_TL_EIN":
			case "STATUS_DIGITAL#STAT_VL_EIN":
			case "STATUS_DIGITAL#STAT_ZUENDUNG1_EIN":
			case "STATUS_DIGITAL#STAT_ZUENDUNG2_EIN":
			case "STATUS_DIGITAL#STAT_ZUENDUNG3_EIN":
			case "STATUS_DIGITAL#STAT_ZUENDUNG4_EIN":
			case "STATUS_DIGITAL#STAT_ZUENDUNG5_EIN":
			case "STATUS_DIGITAL#STAT_ZUENDUNG6_EIN":
			case "STATUS_DIGITAL#STAT_ZUENDUNG7_EIN":
			case "STATUS_DIGITAL#STAT_ZUENDUNG8_EIN": // Boolean (1/0) values
				result = (ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) > 0.5) ? "On" : "Off";
				if (!found) break;

				switch (result) {
					case "On"  : textColor = Android.Graphics.Color.Red;    break;
					case "Off" : textColor = Android.Graphics.Color.Green;  break;
					default    : textColor = Android.Graphics.Color.Yellow; break;
				}
				break;


			case "STATUS_MESSWERTBLOCK_LESEN#STAT_REGENERATION_BLOCKIERUNG_UND_FREIGABE_WERT": // DPF unblocked (actually DPF blocked, which is why the result logic is inverted)
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = (value < 0.5) ? "1" : "0";
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_PFltRgn_numRgn_WERT": // DPF regeneration request
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = ((value > 3.5) && (value < 6.5)) ? "1" : "0";
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_CoEOM_stOpModeAct_WERT": // DPF regeneration status
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = (((int) (value + 0.5) & 0x02) != 0) ? "1" : "0";
				break;
		}


		// textColor formatting for boolean (1/0)
		switch (resultName) {
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_CoEOM_stOpModeAct_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_PFltRgn_numRgn_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_REGENERATION_BLOCKIERUNG_UND_FREIGABE_WERT":
				switch (result) {
					case "1" : textColor = Android.Graphics.Color.Red;    break;
					case "0" : textColor = Android.Graphics.Color.Green;  break;
					default  : textColor = Android.Graphics.Color.Yellow; break;
				}
				break;
		}

		return result;
	}
}
]]>
</code>
</fragment>
