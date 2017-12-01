function GenerateTable(Schedule) {
    var result = "<table>";
    result += "<tr>";
    result += "<th>№</th>";
    result += "<th>Дата оплати до</th>";
    result += "<th>Платіж</th>";
    result += "<th>Проценти</th>";
    result += "<th>Погашення тіла</th>";
    result += "<th>Залишок</th>";
    result += "</tr>";
    for (var i = 0; i < Schedule.length - 1; i++) {
        result += "<tr>";
        result += "<td>" + (i + 1) + "</td>";
        for (var j = 0; j < Schedule[i].length - 1; j++) {
            if (j >= 1)
                result += "<td>" + MaskDecimal(Schedule[i][j]) + "</td>";
            else
                result += "<td>" + Schedule[i][j] + "</td>";
        }
        result += "</tr>";
    }
    result += "</table>";
    return result;
}
function Calculate() {
    ListStawok = new Array(1);
    ListStawok[0] = new Array(2);
    ListStawok[0][0] = parseFloat(NominalRate.value.replace(",", "."));
    ListStawok[0][1] = 1000;

    ProcentKomisiyiZaVydachu = parseFloat(KomisiyaZaVydachuIn.value.replace(",", "."));

    if (classic.checked)
        a = ClassicSchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok);
    else
        a = AnnuitySchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok, 0);

    SumaKomisiyiZaVydachu = parseFloat((parseFloat(Suma.value) * ProcentKomisiyiZaVydachu / 100).toFixed(2));
    CalculatedRealRate = parseFloat((a[a.length - 1][2] + SumaKomisiyiZaVydachu) * 100 * 12 / (parseFloat(Suma.value) * parseFloat(Termin.value))).toFixed(2);
    RealRate.innerHTML = MaskDecimal(CalculatedRealRate) + "%";
    CalculatedRealRateFull = parseFloat((a[a.length - 1][2] + SumaKomisiyiZaVydachu) * 100 / parseFloat(Suma.value)).toFixed(2);

    SumaPlatezhiv.innerHTML = MaskDecimal(a[a.length - 1][1]);
    SumaProcentiv.innerHTML = MaskDecimal(a[a.length - 1][2]) + " (" + MaskDecimal(CalculatedRealRateFull) + "%)";
    KomisiyaZaVydachu.innerHTML = MaskDecimal(SumaKomisiyiZaVydachu) + " (";
    KomisiyaZaVydachuAfter.innerHTML = ")%"
    EffectiveRate.innerHTML = CalculateEffectiveRate(parseInt(Suma.value), SumaKomisiyiZaVydachu, new Date(), a) + "%";
}
function Block(b, ccc) {
    
    var AllButComa = /[^,]/g;
    var AllButDot = /[^.]/g
    var AllButComaAndNumbers = /[,\d]/g;
    if (b.value == '00')
        ccc.value = "0";
    else if (b.value.replace(AllButComa, '').length == 2 || (b.value.replace(AllButComa, '').length == 1 && b.value.replace(AllButDot, '').length == 1))
        ccc.value = ccc.value.substr(0, ccc.value.length - 1);
    else if (b.value.replace(AllButDot, '').length == 1 && b.value.replace(AllButComa, '').length == 0)
        ccc.value = ccc.value.replace('.', ',');
    else if (b.value.replace(AllButComaAndNumbers, '').length == 1)
        ccc.value = ccc.value.substr(0, ccc.value.length - 1);

    var m = b.value.split(",");
    if (typeof (m[0] != "undefined")) {
        if (m[0].length == 3)
            m[0] = m[0].substr(0, m[0].length - 1);
        ccc.value = m[0] + b.value.replace(/[^,]/g, '');
    }
    if (typeof (m[1] != "undefined")) {
        if (m[1].length == 3)
            m[1] = m[1].substr(0, m[1].length - 1);
        ccc.value = ccc.value + m[1];
    }
}

var a;
var ListStawok;
var ProcentKomisiyiZaVydachu;
var CalculatedRealRate;
var CalculatedRealRateFull;

outputTermin.value = Termin.value;
outputSuma.value = MaskInt(Suma.value);

Calculate();
Schedule.innerHTML = GenerateTable(a);

outputTermin.onchange = function () {
    Termin.value = this.value;
    Termin.oninput();
    Schedule.innerHTML = GenerateTable(a);
}

outputTermin.oninput = function () {
    if (this.value == '')
        outputTermin.value = Termin.min;
    else if (parseInt(this.value) > parseInt(Termin.max))
        outputTermin.value = Termin.max;
    else
        outputTermin.value = this.value;

    Termin.value = outputTermin.value;
}

outputSuma.onchange = function () {
    
    Suma.value = this.value.replace(/ /g, '');
    
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}

outputSuma.oninput = function () {
    if (this.value.replace(/[ \d]/g, '').length == 1)
        this.value = this.value.substr(0, this.value.length - 1);
    var n;
    if (this.value != '')
        n = parseInt((this.value).replace(/ /g, ''));
    else
        n = parseInt((Suma.min).replace(/ /g, ''));

    outputSuma.value = MaskInt(n);
    Suma.value = n;
}

Termin.oninput = function () {
    outputTermin.value = MaskInt(this.value);
    Calculate();
}

Termin.onchange = function () {
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
    
}

Suma.oninput = function () {
    outputSuma.value = MaskInt(this.value);

    SumaStrahovky = parseFloat((parseFloat(this.value) * ProcentStrahowky / 100).toFixed(2));
    SumaKomisiyiZaVydachu = parseFloat((parseFloat(this.value) * ProcentKomisiyiZaVydachu / 100).toFixed(2));
    Strahovka.innerHTML = MaskDecimal(SumaStrahovky);
    KomisiyaZaVydachu.innerHTML = MaskDecimal(SumaKomisiyiZaVydachu);

    Calculate();
}

Suma.onchange = function () {
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}

annuity.onchange = function () {
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}


classic.onchange = function () {
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}

KomisiyaZaVydachuIn.oninput = function () {
    Block(this, KomisiyaZaVydachuIn);
}

NominalRate.oninput = function () {
    Block(this, NominalRate);
}

KomisiyaZaVydachuIn.onchange = function () {
    KomisiyaZaVydachuIn.value = MaskDecimal(parseFloat(this.value.replace(',', '.')));
    Calculate();
}

NominalRate.onchange = function () {
    NominalRate.value = MaskDecimal(parseFloat(this.value.replace(',', '.')));
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}
