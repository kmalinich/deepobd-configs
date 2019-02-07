class PageClass {
	public string FormatResult(JobReader.PageInfo pageInfo, MultiMap<string, EdiabasNet.ResultData> resultDict, string resultName, ref Android.Graphics.Color? textColor) {
		bool found;
		double value;
		string result = string.Empty;

		switch (resultName) {
			// Boost pressure actual
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", (value - 1013) / 68.948);
				if (found && value > 35) textColor = Android.Graphics.Color.Red;
				if (!found) result = string.Empty;
				break;

			// Boost pressure target
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_SOLL_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", (value - 1013) / 68.948);
				if (found && value > 35) textColor = Android.Graphics.Color.Red;
				if (!found) result = string.Empty;
				break;

			// Exhaust back pressure
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_DIFFERENZDRUCK_UEBER_PARTIKELFILTER_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = string.Format(ActivityMain.Culture, "{0,7:0.00}", (value - 1013) / 68.948);
				if (found && value > 35) textColor = Android.Graphics.Color.Red;
				if (!found) result = string.Empty;
				break;

			// Oil pressure switch
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_OELDRUCKSCHALTER_EIN_WERT":
				result = ((ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) > 0.5) && found) ? "1" : "0";
				if (found && result == "1") textColor = Android.Graphics.Color.Red;
				if (!found) result = string.Empty;
				break;

			// DPF distance since regeneration
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_STRECKE_SEIT_ERFOLGREICHER_REGENERATION_WERT":
				result = string.Format(ActivityMain.Culture, "{0,6:0.0}", ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) / 1000.0);
				if (!found) result = string.Empty;
				break;

			// DPF regeneration request
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_PFltRgn_numRgn_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = ((value > 3.5) && (value < 6.5) && found) ? "1" : "0";
				if (!found) result = string.Empty;
				break;

			// DPF regeneration status
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_CoEOM_stOpModeAct_WERT":
				value  = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				result = ((((int)(value + 0.5) & 0x02) != 0) && found) ? "1" : "0";
				if (!found) result = string.Empty;
				break;

			// DPF unblocked
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_REGENERATION_BLOCKIERUNG_UND_FREIGABE_WERT":
				result = ((ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) < 0.5) && found) ? "1" : "0";
				if (!found) result = string.Empty;
				break;
		}

		return result;
	}
}
