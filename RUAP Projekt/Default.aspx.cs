using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace RUAP_Projekt
{
    public partial class Default : System.Web.UI.Page
    {
        string apiKey, apiLink, probability, prediction;

        static int anaemiaYesCount = 0, anaemiaNoCount = 0, diabetesYesCount = 0, diabetesNoCount = 0, pressureYesCount = 0, pressureNoCount = 0, sexMaleCount = 0, sexFemaleCount = 0, smokingYesCount = 0, smokingNoCount = 0;
        static List<int> ages = new List<int>();
        static List<int> ejections = new List<int>();
        static List<double> serumCs = new List<double>();
        static List<int> serumSs = new List<int>();

        public static string data = "0 0";
        public static string label = "YES NO";

        protected void Page_Load(object sender, EventArgs e)
        {
            ChangeVariable();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {

            if (IsFieldEmpty())
                lblText.Text = "Required field is empty!";
            else
            {
                lblText.Text = "";
                var options = new RestClientOptions()
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);

                GetLinkAndKey(ddlAlgorithm.SelectedValue);
                var request = new RestRequest(apiLink, Method.Post);
                request.AddHeader("Authorization", apiKey);
                request.AddHeader("Content-Type", "application/json");
                var body = "{\"Inputs\":{\"input1\":{\"ColumnNames\":[\"age\",\"anaemia\",\"creatinine_phosphokinase\",\"diabetes\",\"ejection_fraction\",\"high_blood_pressure\",\"platelets\",\"serum_creatinine\",\"serum_sodium\",\"sex\",\"smoking\",\"time\",\"DEATH_EVENT\"],\"Values\":[[\"" + txtAge.Text + "\",\"" + rblAnaemia.SelectedValue + "\",\"" + txtCreatinine.Text + "\",\"" + rblDiabetes.SelectedValue + "\",\"" + txtEjection.Text + "\",\"" + rblBloodPressure.SelectedValue + "\",\"" + txtPlatelets.Text + "\",\"" + txtSerumC.Text + "\",\"" + txtSerumS.Text + "\",\"" + rblSex.SelectedValue + "\",\"" + rblSmoking.SelectedValue + "\",\"" + txtTime.Text + "\",\"0\"]]}},\"GlobalParameters\":{}}";
                request.AddStringBody(body, DataFormat.Json);

                ParseOutput(client.Execute(request));

                string result = prediction == "1" ? "likely" : "not likely";
                lblText.Text = $"Patient is {result} to suffer hearth failure. Scored probability is: {probability}";
            }

        }

        protected bool IsFieldEmpty()
        {
            return string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(rblAnaemia.SelectedValue) || string.IsNullOrEmpty(txtCreatinine.Text)
                || string.IsNullOrEmpty(rblDiabetes.SelectedValue) || string.IsNullOrEmpty(txtEjection.Text) || string.IsNullOrEmpty(rblBloodPressure.SelectedValue)
                || string.IsNullOrEmpty(txtPlatelets.Text) || string.IsNullOrEmpty(txtSerumC.Text) || string.IsNullOrEmpty(txtSerumS.Text)
                || string.IsNullOrEmpty(rblSex.SelectedValue) || string.IsNullOrEmpty(rblSmoking.SelectedValue) || string.IsNullOrEmpty(txtTime.Text);
        }

        protected void ParseOutput(RestResponse response)
        {
            var results = JObject.Parse(response.Content);
            var values = results["Results"]["output1"]["value"]["Values"].Last();

            probability = Convert.ToDecimal(values.Last().ToString(), CultureInfo.InvariantCulture).ToString("#0.##%");
            prediction = values.ElementAt(values.Count() - 2).ToString();

            if (string.Equals(prediction, "1"))
            {
                if (values[1].ToString() == "1")
                    anaemiaYesCount++;
                else
                    anaemiaNoCount++;

                if (values[3].ToString() == "1")
                    diabetesYesCount++;
                else
                    diabetesNoCount++;

                if (values[5].ToString() == "1")
                    pressureYesCount++;
                else
                    pressureNoCount++;

                if (values[9].ToString() == "1")
                    sexMaleCount++;
                else
                    sexFemaleCount++;

                if (values[10].ToString() == "1")
                    smokingYesCount++;
                else
                    smokingNoCount++;

                ages.Add(Convert.ToInt32(values[0]));
                ejections.Add(Convert.ToInt32(values[4]));
                serumCs.Add(Convert.ToDouble(values[7]));
                serumSs.Add(Convert.ToInt32(values[8]));
            }

            ChangeVariable();
        }

        protected void GetLinkAndKey(string algorithm)
        {
            switch (algorithm)
            {
                case "0":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/3b2b250d84874d3a932d65daf307ea5a/execute?api-version=2.0&details=true";
                    apiKey = "Bearer EK9XIESGnDoxamdrzROK+ujaeAZmphmRZwqIEKFpEBmdc60Y3kDmH9gacKcF2/IeTUQq7SdnsaHU+AMCU4ezAg==";
                    break;

                case "1":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/da7d741526df498ba6b62b204773db54/execute?api-version=2.0&details=true";
                    apiKey = "Bearer SJNmchEZBNwT4npsTPYd185YuIBQoPAaQLZn3NWYbJzJTjmlv/I9GG7L8pJbrPT1zxSWopw5aQFu+AMCajtawg==";
                    break;

                case "2":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/50e95bd203094573971eb0abf9056b39/execute?api-version=2.0&details=true";
                    apiKey = "Bearer qYzYPJMw66Jqb8Mw1CXoiFWgMgQOXSAe+ADjRKTxP3ssvtu7aLTuPG8S8HesoWnF/lIfAqHHlcWD+AMC/iZ6CA==";
                    break;

                case "3":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/973bc5be1cfa458396b574d8158e779c/execute?api-version=2.0&details=true";
                    apiKey = "Bearer F4NF0Np4l4zdpr/pztHVsYdrX0ikFWVt/lJVc2ItQ9pomJSvvweLPwHUOGgah4/0mWpqPPUgsSzQ+AMC6JYV0A==";
                    break;

                case "4":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/1d125756e7d04576b4275f348246349e/execute?api-version=2.0&details=true";
                    apiKey = "Bearer U/dwrkoALhg5kOY0tt2809YmBfuWx/+jQ3TFznvtC8D2DJcEuFd4ictbt4A9g27dMF3GqPMzkB2i+AMC4wH+mA==";
                    break;

                case "5":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/bf4651b9073f4f8f9c94f8b963ce4166/execute?api-version=2.0&details=true";
                    apiKey = "Bearer 5d5VIfXJXtzoAAmBMoIXPEAAxitBMrr/XWEbcwd++XTVfpS7nDhXqwkDZjoK0ISwlrWrgQfu702w+AMCc6JKOA==";
                    break;

                case "6":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/66e45daa14be4345868f16d5ac47697e/execute?api-version=2.0&details=true";
                    apiKey = "Bearer J5g20SaLbVJxXi8PqfG6ALCMiaqbRclL7Lbt25imxJy00QMn/lUtlsLeVzjtGQ8eUwBn0OoTj1D1+AMCyjfEVw==";
                    break;

                case "7":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/30cfa26d952b4a859e84a0f5d25c67a4/execute?api-version=2.0&details=true";
                    apiKey = "Bearer PaMTfX/wNgd72O+Mn6G0DGfmp5PGTJTBRzTAGd7fometTITqVhJ+Ihpolw8hVybueagUKiIJZkuj+AMCLJWeDQ==";
                    break;

                case "8":
                    apiLink = "https://ussouthcentral.services.azureml.net/workspaces/f4139a912af043b2af070ea9ee0a7c79/services/84fcf3b9fd514e8c9b2653e59490b72b/execute?api-version=2.0&details=true";
                    apiKey = "Bearer b1sF15K2Yf16+RsmL5GvlO2+znxVOhERRyBiWyZxT7gj+cZFXUD5gbMVU5dF6OLJC28ClBHGVBuD+AMCIpAHCw==";
                    break;
            }
        }

        protected void ChangeVariable()
        {
            int value1, value2, value3;
            if (ddlVariable.SelectedValue == "0")
            {
                value1 = ages.Where(x => x < 40).Count();
                value2 = ages.Where(x => x >= 40 && x <= 60).Count();
                value3 = ages.Where(x => x > 60).Count();
                data = $"{value1} {value2} {value3}";
                label = "<40 40-60 >60";
            }
            else if (ddlVariable.SelectedValue == "1")
            {
                data = $"{anaemiaYesCount} {anaemiaNoCount}";
                label = "YES NO";
            }
            else if (ddlVariable.SelectedValue == "2")
            {
                data = $"{diabetesYesCount} {diabetesNoCount}";
                label = "YES NO";
            }
            else if (ddlVariable.SelectedValue == "3")
            {
                value1 = ejections.Where(x => x < 30).Count();
                value2 = ejections.Where(x => x >= 30 && x <= 40).Count();
                value3 = ejections.Where(x => x > 40).Count();
                data = $"{value1} {value2} {value3}";
                label = "<30 30-40 >40";
            }
            else if (ddlVariable.SelectedValue == "4")
            {
                data = $"{pressureYesCount} {pressureNoCount}";
                label = "YES NO";
            }
            else if (ddlVariable.SelectedValue == "5")
            {
                value1 = serumCs.Where(x => x < 1).Count();
                value2 = serumCs.Where(x => x >= 1 && x <= 2).Count();
                value3 = serumCs.Where(x => x > 2).Count();
                data = $"{value1} {value2} {value3}";
                label = "<1 1-2 >2";
            }
            else if (ddlVariable.SelectedValue == "6")
            {
                value1 = serumSs.Where(x => x < 130).Count();
                value2 = serumSs.Where(x => x >= 130 && x <= 140).Count();
                value3 = serumSs.Where(x => x > 140).Count();
                data = $"{value1} {value2} {value3}";
                label = "<130 130-140 >140";
            }
            else if (ddlVariable.SelectedValue == "7")
            {
                data = $"{sexMaleCount} {sexFemaleCount}";
                label = "MALE FEMALE";
            }
            else if (ddlVariable.SelectedValue == "8")
            {
                data = $"{smokingYesCount} {smokingNoCount}";
                label = "YES NO";
            }
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            anaemiaYesCount = 0;
            anaemiaNoCount = 0;
            diabetesYesCount = 0;
            diabetesNoCount = 0;
            pressureYesCount = 0;
            pressureNoCount = 0;
            sexMaleCount = 0;
            sexFemaleCount = 0;
            smokingYesCount = 0;
            smokingNoCount = 0;
            ages.Clear();
            ejections.Clear();
            serumCs.Clear();
            serumSs.Clear();

            ChangeVariable();
        }
    }
}