<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RUAP_Projekt.Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hearth Failure Prediction</title>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <div class="wrapper">
        <div class="title">
            HEARTH FAILURE PREDICTION
        </div>

        <form runat="server">
            <div class="container">
                <div class="left">
                    <div class="input_field">
                        <label>Age:</label>
                        <asp:TextBox ID="txtAge" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="input_field">
                        <label>Anaemia:</label>
                        <asp:RadioButtonList ID="rblAnaemia" CssClass="input_radio" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="input_field">
                        <label>Creatinine phosphokinase:</label>
                        <asp:TextBox ID="txtCreatinine" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="input_field">
                        <label>Diabetes:</label>
                        <asp:RadioButtonList ID="rblDiabetes" CssClass="input_radio" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="input_field">
                        <label>Ejection fraction:</label>
                        <asp:TextBox ID="txtEjection" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="input_field">
                        <label>High blood pressure:</label>
                        <asp:RadioButtonList ID="rblBloodPressure" CssClass="input_radio" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="right">
                    <div class="input_field">
                        <label>Platelets:</label>
                        <asp:TextBox ID="txtPlatelets" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="input_field">
                        <label>Serum creatinine:</label>
                        <asp:TextBox ID="txtSerumC" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="input_field">
                        <label>Serum sodium:</label>
                        <asp:TextBox ID="txtSerumS" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                    <div class="input_field">
                        <label>Sex:</label>
                        <asp:RadioButtonList ID="rblSex" CssClass="input_radio" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="1">Male</asp:ListItem>
                            <asp:ListItem Value="0">Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="input_field">
                        <label>Smoking:</label>
                        <asp:RadioButtonList ID="rblSmoking" CssClass="input_radio" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="input_field">
                        <label>Follow-up period:</label>
                        <asp:TextBox ID="txtTime" CssClass="input" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="bottom">
                <asp:Label ID="lblText" CssClass="text" runat="server"></asp:Label>
                <div class="input_field">
                    <label>Choose an algorithm:</label>
                    <asp:DropDownList ID="ddlAlgorithm" CssClass="input" runat="server">
                        <asp:ListItem Value="0">Neural network</asp:ListItem>
                        <asp:ListItem Value="1">Support vector machine</asp:ListItem>
                        <asp:ListItem Value="2">Locally-deep support vector machine</asp:ListItem>
                        <asp:ListItem Value="3">Bayes point machine</asp:ListItem>
                        <asp:ListItem Value="4">Boosted decision tree</asp:ListItem>
                        <asp:ListItem Value="5">Decision forest</asp:ListItem>
                        <asp:ListItem Value="6">Decision jungle</asp:ListItem>
                        <asp:ListItem Value="7">Logistic regression</asp:ListItem>
                        <asp:ListItem Value="8">Averaged perceptron</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="input_field">
                    <asp:Button ID="Submit" CssClass="button" runat="server" Text="SUBMIT" OnClick="Submit_Click"/> 
                </div>
            </div>
            <hr/>
            <div class="chart">
                <div class="chart_header">
                    <p class="subtitle">Visualizating deaths by parameters</p>
                    <div class="input_field">
                        <label>Select a variable:</label>
                        <asp:DropDownList ID="ddlVariable" CssClass="input" AutoPostBack="true" runat="server">
                            <asp:ListItem Value="0">Age</asp:ListItem>
                            <asp:ListItem Value="1">Anaemia</asp:ListItem>
                            <asp:ListItem Value="2">Diabetes</asp:ListItem>
                            <asp:ListItem Value="3">Ejection fraction</asp:ListItem>
                            <asp:ListItem Value="4">High blood pressure</asp:ListItem>
                            <asp:ListItem Value="5">Serum creatinine</asp:ListItem>
                            <asp:ListItem Value="6">Serum sodium</asp:ListItem>
                            <asp:ListItem Value="7">Sex</asp:ListItem>
                            <asp:ListItem Value="8">Smoking</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="chart_body">
                    <canvas class="pie" id="pie"></canvas>
                </div>
                <asp:Button ID="btnReset" CssClass="reset" runat="server" Text="RESET" OnClick="Reset_Click"/>
            </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.min.js"></script>
            <script type="text/javascript"> 
                var config = {
                    type: 'pie',
                    data: {
                        labels: ['',''],
                        datasets: [{
                            data: [0, 0],
                            backgroundColor: ["red", "blue", "green"]
                        }]
                    },
                };

                window.onload = function () {
                    var pie = document.getElementById("pie").getContext('2d');
                    var chartData = '<%=data%>';
                    var chartLabel = '<%=label%>';
                    config.data.datasets.forEach(function (dataset) {
                        dataset.data = chartData.split(' ');
                    });
                    config.data.labels = chartLabel.split(' ');
                    window.myPie = new Chart(pie, config);
                }
            </script>

        </form>
    </div>
</body>
</html>
