<?xml version="1.0" encoding="utf-8"?>
<fragment xmlns="http://www.holeschak.de/BmwDeepObd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.holeschak.de/BmwDeepObd ../BmwDeepObd.xsd">
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


	public string FormatResult(JobReader.PageInfo pageInfo, MultiMap<string, EdiabasNet.ResultData> resultDict, string resultName, ref Android.Graphics.Color? textColor) {
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
			case "STATUS_TZ1#STAT_TZ1_WERT": // Ignition timing
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				if (value > 64) value = value - 6553.5;

				result    = string.Format(ActivityMain.Culture, "{0,4:0.0}", value);
				textColor = val2Rgb(value, 50, 20);
				break;


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

				result    = string.Format(ActivityMain.Culture, "{0,4:0}", value);
				textColor = val2Rgb(value, 400, 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_SOLL_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_DIFFERENZDRUCK_UEBER_PARTIKELFILTER_WERT": // Boost pressure/exhaust back pressure
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				value = (value - ambient_offset) * hpa2psi;

				result    = string.Format(ActivityMain.Culture, "{0,5:0.00}", value);
				textColor = val2Rgb(value, 40, 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_UBATT2_WERT": // Battery voltage
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				value = value / 1000;

				result    = string.Format(ActivityMain.Culture, "{0,5:0.00}", value);
				textColor = val2Rgb(value, 16, 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_OELDRUCKSCHALTER_EIN_WERT": // Oil pressure switch
				result = ((ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) > 0.5) && found) ? "1" : "0";
				if (!found) break;

				switch (result) {
					case "1" : textColor = Android.Graphics.Color.Red;    break;
					case "0" : textColor = Android.Graphics.Color.Green;  break;
					default  : textColor = Android.Graphics.Color.Yellow; break;
				}
				break;


			case "STATUS_MESSWERTBLOCK_LESEN#STAT_STRECKE_SEIT_ERFOLGREICHER_REGENERATION_WERT": // DPF distance since regeneration
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				// Convert km to mi
				value = (value / 1000) * km2mi;

				result = string.Format(ActivityMain.Culture, "{0,6:0.0}", value);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_PFltRgn_numRgn_WERT": // DPF regeneration request
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = ((value > 3.5) && (value < 6.5) && found) ? "1" : "0";
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_CoEOM_stOpModeAct_WERT": // DPF regeneration status
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = ((((int) (value + 0.5) & 0x02) != 0) && found) ? "1" : "0";
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_REGENERATION_BLOCKIERUNG_UND_FREIGABE_WERT": // DPF unblocked
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = ((value < 0.5) && found) ? "1" : "0";
				break;
		}

		return result;
	}
}
]]>
</code>
</fragment>
