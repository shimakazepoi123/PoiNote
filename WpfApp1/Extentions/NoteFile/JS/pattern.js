var List = new Array();
var List_Paragraph = new Array();

var pattern = {
    Import:(text) =>{
        pattern.text = text;
    },

    Header: () => {
        var search1 = pattern.text.match("(#+)\\s+(.*)");

        if (!search1){
            return false;
        }
        else{
            var header = ["Header",search1[1].length,search1[2]];
            List.push(header);
        }
    },

    Paragraph: () => {
            var Bold = new RegExp("\\*{2}","g");
            var Italic = new RegExp("_","g");
            var b = pattern.text.match(Bold);
            var i = pattern.text.match(Italic);

            var f;
            if (pattern.text == ""){
                f = "&nbsp";
            }
            else{
                f = pattern.text;
            }

            function inner(array1,symbol,symbol1,symbol2)
            {
                if (array1){
                    var c = array1.length;
    
                    for (i=0;i<c;i++){
                        if (i % 2 == 0 ){
                            f = f.replace(symbol,symbol1);
                        }
                        else{
                            f = f.replace(symbol,symbol2);
                        }
                    }
                }
            }
            inner(b,"**","<strong>","</strong>");
            inner(i,"_","<em>","</em>");

            var para = ["Paragraph",f]
            List.push(para)
    },
}

var replace = function replace_html(){
    List.forEach(element => {
        switch (element[0]){
            case "Header":
                var title = element[1];
                List_Paragraph.push(`<h${title}>${element[2]}</h${title}>`);
                break;
            case "Paragraph":
                List_Paragraph.push("<p>"+element[1]+"</p>");
                break;
        } 
    });
    var Html = List_Paragraph.join("\n");

    List = [];
    List_Paragraph = [];

    return Html;

}

/*程序入口*/ 
var to_html = function markdown_to_html(input){
    for (i=0;i<input.length;i++){
        pattern.Import(input[i]);
        var a = pattern.Header();
        if (a == false){
            pattern.Paragraph();
        }
    }
    var replace_html = replace();
    return replace_html;

}


/*执行*/ 
var f = document.getElementsByTagName("textarea")[0];
var d = document.getElementsByClassName("text")[0];
var preview = document.getElementById("preview");

/*删除原节点*/
f.addEventListener("input",()=>{
    d.childNodes.forEach((item)=>{
        d.removeChild(item);
    })

    var replace_html = to_html(f.value.split("\n"));
    
    d.innerHTML = replace_html;
})