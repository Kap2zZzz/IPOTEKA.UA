function GeneratePropositions(BankArray, MatrixArray, CreditSum, Termin, Product, radioClassic) {
    var AmountTitle;
    if (radioClassic.checked)
        AmountTitle = "Платіж першого місяця";
    else
        AmountTitle = "Місячний платіж";

    var result =
        "<table id=\"Proposition\" style=\"width:80%;\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\">" +
            "<tr>" +
                "<th>Банк</th>" +
                "<th>Ставка, %</th>" +
                "<th>Ефективна ставка, %</th>" +
                "<th>Комісія за видачу</th>" +
                "<th>" + AmountTitle + "</th>" +
            "</tr>";
    var propositions = 0;
    for (var i = 0; i < BankArray.length; i++) {
        for (var j = 0; j < BankArray.length * CountProducts; j++) {
            if (BankArray[i] == MatrixArray[0][j] && Product == MatrixArray[1][j] && parseFloat(CreditSum) <= MatrixArray[5][j] && parseInt(Termin) >= MatrixArray[6][j] && parseInt(Termin) <= MatrixArray[7][j]) {
                propositions += 1;

                var RateList = new Array(1);
                RateList[0] = new Array(2);
                RateList[0][0] = parseFloat(MatrixArray[2][j]);
                RateList[0][1] = 1000;

                var Schedule;
                if (radioClassic.checked)
                    Schedule = ClassicSchedule(parseFloat(CreditSum), parseInt(Termin), RateList);
                else
                    Schedule = AnnuitySchedule(parseFloat(CreditSum), parseInt(Termin), RateList, 0);

                var Commission = parseFloat((parseFloat(CreditSum) * parseFloat(MatrixArray[3][j]) / 100).toFixed(2));
                result +=
                    "<tr>" +
                        "<td>" + MatrixArray[0][j] + "</th>" +
                        "<td>" + MatrixArray[2][j] + "</th>" +
                        "<td>" + CalculateEffectiveRate(parseFloat(CreditSum), Commission, new Date(), Schedule) + "</th>" +
                        "<td>" + MaskDecimal(Commission) + "</th>" +
                        "<td>" + MaskDecimal(Schedule[1][1]) + "</th>" +
                    "</tr>";
                break;
            }
        }
    }
    if (propositions == 0) {
        result += "<tr>" +
                        "<td colspan=\"6\">" +
                            "Пропозиції за вибраними параметрами відсутні. Спробуйте змінити параметри." +
                        "</td>" +
                    "</tr>";
    }
    result += "</table>";
    return result;
}

var b = Result.getElementsByTagName("legend");
var Bank = new Array(b.length);
for (var i = 0; i < Bank.length; i++) {
    Bank[i] = b[i].innerHTML;
}
var CountParameters = 8; var CountProducts = 5;
var Matrix = new Array(CountParameters);
for (var i = 0; i < CountParameters; i++) {
    Matrix[i] = new Array(CountProducts * Bank.length);
}
for (var i = 0; i < Bank.length; i++) {
    var p = Result.getElementsByClassName(b[i].innerHTML.replace(" ", ""));
    var k = 0;
    for (var j = i * CountProducts; j < i * CountProducts + CountProducts; j++) {
        if (k < p.length) {
            Matrix[0][j] = b[i].innerHTML;
            Matrix[1][j] = p[k].innerHTML;

            Matrix[2][j] = parseFloat(p[k + 1].innerHTML);
            Matrix[3][j] = parseFloat(p[k + 2].innerHTML);
            Matrix[4][j] = parseFloat(p[k + 3].innerHTML);
            Matrix[5][j] = parseFloat(p[k + 4].innerHTML);
            Matrix[6][j] = parseInt(p[k + 5].innerHTML);
            Matrix[7][j] = parseInt(p[k + 6].innerHTML);
        }
        else {
            Matrix[0][j] = b[i].innerHTML;
            Matrix[1][j] = "NA";
            Matrix[2][j] = -1;
            Matrix[3][j] = -1;
            Matrix[4][j] = -1;
            Matrix[5][j] = -1;
            Matrix[6][j] = -1;
            Matrix[7][j] = -1;
        }
        k += 7;
    }
}

Result.innerHTML = GeneratePropositions(Bank, Matrix, sliderCreditSum.value, sliderTerm.value, ProductType.value, classic);

fieldTerm.value = sliderTerm.value; // Display the default slider value
fieldCreditSum.value = MaskInt(sliderCreditSum.value); // Display the default slider value

sliderTerm.oninput = function () {
    fieldTerm.value = this.value;
    Result.innerHTML = GeneratePropositions(Bank, Matrix, sliderCreditSum.value, this.value, ProductType.value, classic);
}

sliderCreditSum.oninput = function () {
    fieldCreditSum.value = MaskInt(this.value);
    Result.innerHTML = GeneratePropositions(Bank, Matrix, this.value, sliderTerm.value, ProductType.value, classic);
}

fieldTerm.oninput = function () {
    if (this.value == '')
        fieldTerm.value = sliderTerm.min;
    else if (parseInt(this.value) > parseInt(sliderTerm.max))
        fieldTerm.value = sliderTerm.max;
    else
        fieldTerm.value = this.value;
    sliderTerm.value = fieldTerm.value;
}

fieldCreditSum.oninput = function () {
    if (this.value.replace(/[ \d]/g, '').length == 1)
        this.value = this.value.substr(0, this.value.length - 1);

    var n;
    if (this.value != '')
        n = parseInt((this.value).replace(/ /g, ''));
    else
        n = parseInt((sliderCreditSum.min).replace(/ /g, ''));

    fieldCreditSum.value = MaskInt(n);
    sliderCreditSum.value = n;
}

fieldTerm.onchange = function () {
    sliderTerm.value = this.value;
    sliderTerm.oninput();
}

fieldCreditSum.onchange = function () {
    sliderCreditSum.value = parseInt((this.value).replace(/ /g, ''));
    sliderCreditSum.oninput();
}

ProductType.onchange = function () {
    Result.innerHTML = GeneratePropositions(Bank, Matrix, sliderCreditSum.value, sliderTerm.value, this.value, classic);
}

annuity.onchange = function () {
    Result.innerHTML = GeneratePropositions(Bank, Matrix, sliderCreditSum.value, sliderTerm.value, ProductType.value, classic);
}


classic.onchange = function () {
    Result.innerHTML = GeneratePropositions(Bank, Matrix, sliderCreditSum.value, sliderTerm.value, ProductType.value, classic);
}