function Format(symbol,text){
    return symbol + text + symbol;
}

var ProcessText = {
    TextImport:(text)=>{
        ProcessText.text = text;
    },
    /*ts:title类型*/ 
    Header:(ts)=>{
        return "#".repeat(ts) + " " + ProcessText.text;
    },
    Bold:()=>{
        return Format("**",ProcessText.text);
    },
    Italic:()=>{
        return Format("_",ProcessText.text);
    },

}

var TextArea1 = document.getElementsByTagName("textarea")[0];

/*非标题点击事件*/ 
function Event1(element,name,headerclass=0){
    element.addEventListener("click",()=>{
        /*获取选择的字符，并将其输入到参数中*/
        var Selection = TextArea1.value.substring(TextArea1.selectionStart,TextArea1.selectionEnd);
        ProcessText.TextImport(Selection);

        switch (name) {
            case "Header":
                var Change = ProcessText["Header"](headerclass);
                break;
            default:
                var Change = ProcessText[name]();
                break;
        }
        TextArea1.setRangeText(Change);
    })
}

/*标题点击事件*/
var EventHeader = () => {
    var Header = document.getElementsByClassName("Header");
    for (let element of Header){
        var HeaderClass =  element.id.split("")[1];
        Event1(element,"Header",HeaderClass);
    };
}

/*计算字数*/
var TextAreaCount = () => {
    var CountElement = document.getElementsByClassName("Count")[0];
    TextArea1.addEventListener("input",()=>{
        var Count = TextArea1.value.length;
        CountElement.innerHTML = "<p>字数："+ Count + "</p>";
    })
}

var bold = document.getElementById("Bold");
var italic = document.getElementById("Italic");
Event1(bold,"Bold");
Event1(italic,"Italic");
TextAreaCount();
EventHeader();


