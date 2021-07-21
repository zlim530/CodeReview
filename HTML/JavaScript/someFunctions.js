class Point{
    constructor(name){
        this.name = name;
        this.path = [];
        this.children = [];
    }
}
var n1 = new Point('1');
var n2 = new Point('2');
var n3 = new Point('3');
var n4 = new Point('4');
var n5 = new Point('5');

n1.children = [n2,n4];
n1.children = [n1,n3,n5];
n3.children = [n2,n5];
n4.children = [n1,n2];
n5.children = [n2,n3,n4];

function bfs(root){
    var queue = [];
    var set = new Set();
    set.add(root);
    queue.unshift(root);
    root.path = [root];
    while (queue.length > 0) {
        var cur = queue.pop();
        cur.children.forEach(it => {
                if (!set.has(it)) {
                    queue.unshift(it);
                    set.add(it);
                    it.path = [...cur.path,it];
                }
        });
    }
}



function checkedType(target) {
    return Object.prototype.toString.call(target).slice(8,-1)
}

function clone(target) {
    let result, targetType = checkedType(target)
    if (targetType == 'Object') {
        result = {}
    } else if (targetType == 'Array') {
        result = []
    } else {
        return target
    }

    for (let i in target) {
        let value = target[i]
        if (checkedType(value) == 'Object' ||
            checkedType(value) == 'Array') {
                result[i] = clone(value)
        } else {
            result[i] = value
        }
    }
    return result
}