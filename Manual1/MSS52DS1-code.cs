<code show_warnings="true">
<![CDATA[
class PageClass {
	public static Android.Graphics.Color intRgb(double r, double g, double b) {
		return Android.Graphics.Color.Rgb((int) r, (int) g, (int) b);
	}

	public string FormatResult(JobReader.PageInfo pageInfo, MultiMap<string, EdiabasNet.ResultData> resultDict, string resultName, ref Android.Graphics.Color? textColor) {
		bool found;
		double value;

		double ambient_offset = 982;
		double hpa2psi        = 68.948;
		double bar2psi        = 14.504;

		string result = string.Empty;

		switch (resultName) {
			case "STATUS_TZ1#STAT_TZ1_WERT": // Ignition timing
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				if (value > 64) value = value - 6553;

				result = string.Format(ActivityMain.Culture, "{0,4:0.0}", value);

				textColor = intRgb(241, 196, 15);
				break;

			case "STATUS_RF#STAT_RF_WERT": // RF %
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,3:0.0}", value);

				if (value > 100) { textColor = intRgb(255,   0,   0); break; }
				if (value == 50) { textColor = intRgb(0,   255,   0); break; }
				if (value ==  0) { textColor = intRgb(0,     0, 255); break; }

				if (value < 50) {
					textColor = intRgb(0, (value * 5.1), (255 - (value * 5.1)));
					break;
				}

				textColor = intRgb((value * 5.1), (255 - (value * 5.1)), 0);
				break;


			case "STATUS_L_SONDE#STAT_L_SONDE_WERT":
			case "STATUS_L_SONDE_2#STAT_L_SONDE_2_WERT": // Lambda sensor voltage
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,5:0.000}", value);

				if (value >  1.2) { textColor = intRgb(255,   0,   0); break; }
				if (value == 0.6) { textColor = intRgb(0,   255,   0); break; }
				if (value == 0.0) { textColor = intRgb(0,     0, 255); break; }

				// RGB multiplier reasoning:
				// Max value = 1.2
				// 510 / 1.2 = 425

				if (value < 0.60) {
					textColor = intRgb(0, (value * 425), (255 - (value * 425)));
					break;
				}

				textColor = intRgb((value * 425), (255 - (value * 425)), 0);
				break;

			case "STATUS_ADD#STAT_ADD_WERT":
			case "STATUS_ADD_2#STAT_ADD_2_WERT": // Lambda additive
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				if (value > 64) value = value - 131.072;

				result = string.Format(ActivityMain.Culture, "{0,5:0.000}", value);


				if (value >  30) { textColor = Android.Graphics.Color.Red;    break; }
				if (value >  20) { textColor = Android.Graphics.Color.Yellow; break; }
				if (value >  10) { textColor = Android.Graphics.Color.Green;  break; }
				if (value >   0) { textColor = Android.Graphics.Color.White;  break; }
				if (value > -10) { textColor = Android.Graphics.Color.Green;  break; }
				if (value > -20) { textColor = Android.Graphics.Color.Yellow; break; }

				textColor = Android.Graphics.Color.Red;
				break;

			case "STATUS_INT#STAT_INT_WERT":
			case "STATUS_INT_2#STAT_INT_2_WERT": // Lambda integral
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,5:0.000}", value);

				if (value > 0.90) { textColor = Android.Graphics.Color.Yellow; break; }
				if (value > 0.99) { textColor = Android.Graphics.Color.Green;  break; }
				if (value > 1.10) { textColor = Android.Graphics.Color.Yellow; break; }
				if (value > 1.20) { textColor = Android.Graphics.Color.Red;    break; }

				textColor = Android.Graphics.Color.Red;
				break;

			case "STATUS_LAMBDA_MUL_1#STAT_LAMBDA_MUL_1_WERT":
			case "STATUS_LAMBDA_MUL_2#STAT_LAMBDA_MUL_2_WERT": // Lambda multiplicative
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,6:0.000}", value);

				if (value > 0.90) { textColor = Android.Graphics.Color.Yellow; break; }
				if (value > 0.99) { textColor = Android.Graphics.Color.Green;  break; }
				if (value > 1.10) { textColor = Android.Graphics.Color.Yellow; break; }
				if (value > 1.20) { textColor = Android.Graphics.Color.Red;    break; }

				textColor = Android.Graphics.Color.Red;
				break;


			case "STATUS_KUEHLW_AUSL_TEMPERATUR#STAT_KUEHLW_AUSL_TEMPERATUR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_CEngDsT_tSens_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_MOTOROEL_TEMPERATUR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_MOTORTEMPERATUR_WERT": // Radiator outlet/coolant/oil/refrigerant temp
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,3:0}", value);

				if (value > 100) { textColor = intRgb(255,   0,   0); break; }
				if (value == 50) { textColor = intRgb(0,   255,   0); break; }
				if (value ==  0) { textColor = intRgb(0,     0, 255); break; }

				if (value < 50) {
					textColor = intRgb(0, (value * 5.1), (255 - (value * 5.1)));
					break;
				}

				textColor = intRgb((value * 5.1), (255 - (value * 5.1)), 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_KRAFTSTOFFTEMPERATUR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_UMGEBUNGSTEMPERATUR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADELUFTTEMPERATUR_WERT": // Ambient air/intake air/fuel temp
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,3:0}", value);

				// RGB multiplier reasoning:
				// Max value = 40
				// 40 / 0.4  = 100
				// 5.1 / 0.4 = 12.75

				if (value >  40) { textColor = intRgb(255,   0,   0); break; }
				if (value == 20) { textColor = intRgb(0,   255,   0); break; }
				if (value ==  0) { textColor = intRgb(0,     0, 255); break; }

				if (value < 20) {
					textColor = intRgb(0, (value * 12.75), (255 - (value * 12.75)));
					break;
				}

				textColor = intRgb((value * 12.75), (255 - (value * 12.75)), 0);
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_ABGASTEMPERATUR_VOR_KATALYSATOR_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_ABGASTEMPERATUR_VOR_PARTIKELFILTER_1_WERT": // Exhaust temp
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,3:0}", value);

				textColor = Android.Graphics.Color.Blue;
				if (value > 100) textColor = Android.Graphics.Color.White;
				if (value > 300) textColor = Android.Graphics.Color.Green;
				if (value > 500) textColor = Android.Graphics.Color.Yellow;
				if (value > 700) textColor = Android.Graphics.Color.Red;
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_AFS_dmSens_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LUFTMASSE_PRO_HUB_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LUFTMASSE_SOLL_WERT": // Air mass
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,4:0}", value);

				textColor = Android.Graphics.Color.Blue;
				if (value >  300) textColor = Android.Graphics.Color.White;
				if (value >  600) textColor = Android.Graphics.Color.Green;
				if (value >  900) textColor = Android.Graphics.Color.Yellow;
				if (value > 1200) textColor = Android.Graphics.Color.Red;
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_RAILDRUCK_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_RAILDRUCK_SOLL_WERT": // Fuel rail pressure
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,4:0}", (value / bar2psi));

				textColor = Android.Graphics.Color.White;
				if ((value / bar2psi) > 100) textColor = Android.Graphics.Color.Blue;
				if ((value / bar2psi) > 200) textColor = Android.Graphics.Color.Green;
				if ((value / bar2psi) > 300) textColor = Android.Graphics.Color.Yellow;
				if ((value / bar2psi) > 400) textColor = Android.Graphics.Color.Red;
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_LADEDRUCK_SOLL_WERT":
			case "STATUS_MESSWERTBLOCK_LESEN#STAT_DIFFERENZDRUCK_UEBER_PARTIKELFILTER_WERT": // Boost pressure/exhaust back pressure
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,5:0.00}", ((value - ambient_offset) / hpa2psi));

				textColor = Android.Graphics.Color.White;
				if (((value - ambient_offset) / hpa2psi) > 10) textColor = Android.Graphics.Color.Blue;
				if (((value - ambient_offset) / hpa2psi) > 20) textColor = Android.Graphics.Color.Green;
				if (((value - ambient_offset) / hpa2psi) > 30) textColor = Android.Graphics.Color.Yellow;
				if (((value - ambient_offset) / hpa2psi) > 40) textColor = Android.Graphics.Color.Red;
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_UBATT2_WERT": // Battery voltage
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,5:0.00}", value / 1000);

				textColor = Android.Graphics.Color.Red;
				if ((value / 1000) > 10) textColor = Android.Graphics.Color.Yellow;
				if ((value / 1000) > 12) textColor = Android.Graphics.Color.White;
				if ((value / 1000) > 13) textColor = Android.Graphics.Color.Green;
				if ((value / 1000) > 15) textColor = Android.Graphics.Color.Yellow;
				if ((value / 1000) > 16) textColor = Android.Graphics.Color.Red;
				break;

			case "STATUS_MESSWERTBLOCK_LESEN#STAT_OELDRUCKSCHALTER_EIN_WERT": // Oil pressure switch
				result = ((ActivityMain.GetResultDouble(resultDict, resultName, 0, out found) > 0.5) && found) ? "1" : "0";
				if (!found) break;

				textColor = Android.Graphics.Color.White;
				if (result == "1") textColor = Android.Graphics.Color.Red;

				break;


			case "STATUS_MESSWERTBLOCK_LESEN#STAT_STRECKE_SEIT_ERFOLGREICHER_REGENERATION_WERT": // DPF distance since regeneration
				value = ActivityMain.GetResultDouble(resultDict, resultName, 0, out found);
				if (!found) break;

				result = string.Format(ActivityMain.Culture, "{0,6:0.0}", value / 1000.0);

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
