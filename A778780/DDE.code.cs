class PageClass {
	public string FormatResult(JobReader.PageInfo pageInfo, MultiMap<string, EdiabasNet.ResultData> resultDict, string resultName, ref Android.Graphics.Color? textColor) {
		bool found;
		double value;

		double ambient_offset = 982;
		double hpa2psi        = 68.948;
		double bar2psi        = 14.504;

		string result = string.Empty;

		switch (resultName) {
			// Battery voltage
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_UBATT2_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value / 1000);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Red;
				if ((value / 1000) > 10) textColor = Android.Graphics.Color.Yellow;
				if ((value / 1000) > 12) textColor = Android.Graphics.Color.White;
				if ((value / 1000) > 13) textColor = Android.Graphics.Color.Green;
				if ((value / 1000) > 15) textColor = Android.Graphics.Color.Yellow;
				if ((value / 1000) > 16) textColor = Android.Graphics.Color.Red;
				break;

			// Coolant temp
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_MOTORTEMPERATUR_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value > 50)  textColor = Android.Graphics.Color.White;
				if (value > 70)  textColor = Android.Graphics.Color.Green;
				if (value > 85)  textColor = Android.Graphics.Color.Yellow;
				if (value > 100) textColor = Android.Graphics.Color.Red;
				break;

			// Oil temp
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_MOTOROEL_TEMPERATUR_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value > 50)  textColor = Android.Graphics.Color.White;
				if (value > 70)  textColor = Android.Graphics.Color.Green;
				if (value > 85)  textColor = Android.Graphics.Color.Yellow;
				if (value > 100) textColor = Android.Graphics.Color.Red;
				break;

			// Fuel temp
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_KRAFTSTOFFTEMPERATUR_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value > 10) textColor = Android.Graphics.Color.White;
				if (value > 20) textColor = Android.Graphics.Color.Green;
				if (value > 30) textColor = Android.Graphics.Color.Yellow;
				if (value > 40) textColor = Android.Graphics.Color.Red;
				break;

			// Ambient air temp
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_UMGEBUNGSTEMPERATUR_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value > 10) textColor = Android.Graphics.Color.White;
				if (value > 20) textColor = Android.Graphics.Color.Green;
				if (value > 30) textColor = Android.Graphics.Color.Yellow;
				if (value > 40) textColor = Android.Graphics.Color.Red;
				break;

			// Ambient air pressure
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_UMGEBUNGSDRUCK_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", (value / hpa2psi));

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Red;
				if ((value / hpa2psi) > 13)   textColor = Android.Graphics.Color.Yellow;
				if ((value / hpa2psi) > 14)   textColor = Android.Graphics.Color.White;
				if ((value / hpa2psi) > 14.5) textColor = Android.Graphics.Color.Green;
				if ((value / hpa2psi) > 15)   textColor = Android.Graphics.Color.Yellow;
				if ((value / hpa2psi) > 16)   textColor = Android.Graphics.Color.Red;
				break;

			// Intake air temp
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADELUFTTEMPERATUR_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value > 10) textColor = Android.Graphics.Color.White;
				if (value > 20) textColor = Android.Graphics.Color.Green;
				if (value > 30) textColor = Android.Graphics.Color.Yellow;
				if (value > 40) textColor = Android.Graphics.Color.Red;
				break;

			// Exhaust temp before catalyst
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_ABGASTEMPERATUR_VOR_KATALYSATOR_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value > 100) textColor = Android.Graphics.Color.White;
				if (value > 300) textColor = Android.Graphics.Color.Green;
				if (value > 500) textColor = Android.Graphics.Color.Yellow;
				if (value > 700) textColor = Android.Graphics.Color.Red;
				break;

			// Exhaust temp before filter
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_ABGASTEMPERATUR_VOR_PARTIKELFILTER_1_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value > 100) textColor = Android.Graphics.Color.White;
				if (value > 300) textColor = Android.Graphics.Color.Green;
				if (value > 500) textColor = Android.Graphics.Color.Yellow;
				if (value > 700) textColor = Android.Graphics.Color.Red;
				break;

			// Engine refrigerant temp
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_CEngDsT_tSens_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value > 20) textColor = Android.Graphics.Color.White;
				if (value > 40) textColor = Android.Graphics.Color.Green;
				if (value > 60) textColor = Android.Graphics.Color.Yellow;
				if (value > 80) textColor = Android.Graphics.Color.Red;
				break;

			// Air mass
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_AFS_dmSens_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value >  300) textColor = Android.Graphics.Color.White;
				if (value >  600) textColor = Android.Graphics.Color.Green;
				if (value >  900) textColor = Android.Graphics.Color.Yellow;
				if (value > 1200) textColor = Android.Graphics.Color.Red;
				break;

			// Air mass actual
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LUFTMASSE_PRO_HUB_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value >  300) textColor = Android.Graphics.Color.White;
				if (value >  600) textColor = Android.Graphics.Color.Green;
				if (value >  900) textColor = Android.Graphics.Color.Yellow;
				if (value > 1200) textColor = Android.Graphics.Color.Red;
				break;

			// Air mass set point
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LUFTMASSE_SOLL_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,6:0.00}", value);

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.Blue;
				if (value >  300) textColor = Android.Graphics.Color.White;
				if (value >  600) textColor = Android.Graphics.Color.Green;
				if (value >  900) textColor = Android.Graphics.Color.Yellow;
				if (value > 1200) textColor = Android.Graphics.Color.Red;
				break;

			// Fuel rail pressure actual
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_RAILDRUCK_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", (value / bar2psi));

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.White;
				if ((value / bar2psi) > 100) textColor = Android.Graphics.Color.Blue;
				if ((value / bar2psi) > 200) textColor = Android.Graphics.Color.Green;
				if ((value / bar2psi) > 300) textColor = Android.Graphics.Color.Yellow;
				if ((value / bar2psi) > 400) textColor = Android.Graphics.Color.Red;
				break;

			// Fuel rail pressure target
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_RAILDRUCK_SOLL_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", (value / bar2psi));

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.White;
				if ((value / bar2psi) > 100) textColor = Android.Graphics.Color.Blue;
				if ((value / bar2psi) > 200) textColor = Android.Graphics.Color.Green;
				if ((value / bar2psi) > 300) textColor = Android.Graphics.Color.Yellow;
				if ((value / bar2psi) > 400) textColor = Android.Graphics.Color.Red;
				break;

			// Boost pressure actual
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", ((value - ambient_offset) / hpa2psi));

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.White;
				if (((value - ambient_offset) / hpa2psi) > 10) textColor = Android.Graphics.Color.Blue;
				if (((value - ambient_offset) / hpa2psi) > 20) textColor = Android.Graphics.Color.Green;
				if (((value - ambient_offset) / hpa2psi) > 30) textColor = Android.Graphics.Color.Yellow;
				if (((value - ambient_offset) / hpa2psi) > 40) textColor = Android.Graphics.Color.Red;
				break;

			// Boost pressure target
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_SOLL_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", ((value - ambient_offset) / hpa2psi));

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.White;
				if (((value - ambient_offset) / hpa2psi) > 10) textColor = Android.Graphics.Color.Blue;
				if (((value - ambient_offset) / hpa2psi) > 20) textColor = Android.Graphics.Color.Green;
				if (((value - ambient_offset) / hpa2psi) > 30) textColor = Android.Graphics.Color.Yellow;
				if (((value - ambient_offset) / hpa2psi) > 40) textColor = Android.Graphics.Color.Red;
				break;

			// Exhaust back pressure
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_DIFFERENZDRUCK_UEBER_PARTIKELFILTER_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", ((value - ambient_offset) / hpa2psi));

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.White;
				if (((value - ambient_offset) / hpa2psi) > 10) textColor = Android.Graphics.Color.Blue;
				if (((value - ambient_offset) / hpa2psi) > 20) textColor = Android.Graphics.Color.Green;
				if (((value - ambient_offset) / hpa2psi) > 30) textColor = Android.Graphics.Color.Yellow;
				if (((value - ambient_offset) / hpa2psi) > 40) textColor = Android.Graphics.Color.Red;
				break;

			// Oil pressure switch
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_OELDRUCKSCHALTER_EIN_WERT":
				result = ((ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) > 0.5) && found) ? "1" : "0";

				if (!found) { result = string.Empty; break; }

				textColor = Android.Graphics.Color.White;
				if (result == "1") textColor = Android.Graphics.Color.Red;
				break;

			// DPF distance since regeneration
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_STRECKE_SEIT_ERFOLGREICHER_REGENERATION_WERT":
				result = string.Format(ActivityMain.Culture, "{0,6:0.0}", ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) / 1000.0);

				if (!found) break;
				break;

			// DPF regeneration request
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_PFltRgn_numRgn_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = ((value > 3.5) && (value < 6.5) && found) ? "1" : "0";

				if (!found) { result = string.Empty; break; }
				break;

			// DPF regeneration status
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_CoEOM_stOpModeAct_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = ((((int)(value + 0.5) & 0x02) != 0) && found) ? "1" : "0";
				if (!found) { result = string.Empty; break; }
				break;

			// DPF unblocked
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_REGENERATION_BLOCKIERUNG_UND_FREIGABE_WERT":
				result = ((ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) < 0.5) && found) ? "1" : "0";
				if (!found) { result = string.Empty; break; }
				break;
		}

		return result;
	}
}
