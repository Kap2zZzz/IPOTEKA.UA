function GeneratePropositions(BankArray, MatrixArray, CreditSum, Termin, Product, radioClassic) {
    var AmountTitle;
    if (radioClassic.checked)
        AmountTitle = "Платіж першого місяця";
    else
        AmountTitle = "Місячний платіж";

    var result =
        "<table id=\"Proposition\" style=\"width:80%;\" border=\"0\" cellspacing=\"0\" cellpadding=\"1\">" +
            "<tr>" +
                "<th width='15%'>Банк</th>" +
                "<th width='10%'>Ставка, %</th>" +
                "<th width='25%'>Ефективна ставка, %</th>" +
                "<th width='25%'>Комісія за видачу</th>" +
                "<th width='25%'>" + AmountTitle + "</th>" +
            "</tr>";
    var propositions = 0;
    for (var i = 0; i < BankArray.length; i++) {
        for (var j = 0; j < BankArray.length * CountProducts; j++) {
            if (BankArray[i] == MatrixArray[0][j] && Product == MatrixArray[1][j] && parseFloat(CreditSum) <= MatrixArray[5][j] && parseInt(Termin) >= MatrixArray[6][j] && parseInt(Termin) <= MatrixArray[7][j]) {
                propositions += 1;

                var RateList = parseFloat(MatrixArray[2][j]);

                var Schedule;
                if (radioClassic.checked)
                    Schedule = ClassicSchedule(parseFloat(CreditSum), parseInt(Termin), RateList);
                else
                    Schedule = AnnuitySchedule(parseFloat(CreditSum), parseInt(Termin), RateList, 0);

                var Commission = parseFloat((parseFloat(CreditSum) * parseFloat(MatrixArray[3][j]) / 100).toFixed(2));
                result +=
                    "<tr>" +
                        "<td>" + MatrixArray[0][j] + "</th>" +
                        "<td>" + MaskDecimal(MatrixArray[2][j]) + "</th>" +
                        "<td>" + CalculateEffectiveRate(parseFloat(CreditSum), Commission, new Date(), Schedule) + "</th>" +
                        "<td>" + MaskDecimal(Commission) + " (" + MaskDecimal(MatrixArray[3][j]) + ")" + "</th>" +
                        "<td>" + MaskDecimal(Schedule[0][0]) + "</th>" +
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
    else if (propositions > 0) {
        result += "<tr>" +
                    "<td colspan='5' style='border:none; padding-bottom:0px;'>" +
                            "<div style='text-align:right;'>" +
                                    "<button type='submit' name='button' value='Подати заявку'>" +
                                        "Подати заявку <i class='fa fa-user-circle' style='font-size: 1.5em; margin-left: 5px; vertical-align: bottom;'></i>" +
                                    "</button>" +
                            "</div>" +
                   "</td>" +
                 "</tr>"
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

fieldTerm.value = sliderTerm.value;
fieldCreditSum.value = MaskInt(sliderCreditSum.value);

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
    if (this.value != '') {
        if (this.value.charAt(this.value.length - 1) == ' ' || this.value.match(/[1-9][\d ]*/g) != this.value)
            this.value = this.value.substr(0, this.value.length - 1);
        else {
            this.value = this.value.replace(/ /g, '');
            sliderCreditSum.value = this.value;
            this.value = MaskInt(this.value);
        }
    }
    else
        sliderCreditSum.value = sliderCreditSum.min;
}

fieldTerm.onchange = function () {
    sliderTerm.value = this.value;
    sliderTerm.oninput();
}

fieldCreditSum.onchange = function () {
    if (this.value != '') {
        sliderCreditSum.value = this.value.replace(/ /g, '');
        if (parseInt(this.value.replace(/ /g, '')) < parseInt(sliderCreditSum.min))
            fieldCreditSum.value = MaskInt(sliderCreditSum.min);
        else if (parseInt(this.value.replace(/ /g, '')) > parseInt(sliderCreditSum.max))
            fieldCreditSum.value = MaskInt(sliderCreditSum.max);
    }
    else {
        sliderCreditSum.value = sliderCreditSum.min;
        this.value = MaskInt(sliderCreditSum.min);
    }
    Result.innerHTML = GeneratePropositions(Bank, Matrix, sliderCreditSum.value, sliderTerm.value, ProductType.value, classic);
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