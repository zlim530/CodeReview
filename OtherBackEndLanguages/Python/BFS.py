graph = {
    "A": ["B","C"],
    "B": ["A","C","D"],
    "C": ["A","B","D","E"],
    "D": ["B","C","E","F"],
    "E": ["C","D"],
    "F": ["D"],
}

def BFS(graph, s):
    queue = []
    queue.append(s)
    # 已经访问过的节点
    seen = set()
    seen.add(s)
    parent = {s : None}


    while(len(queue) > 0):
        vertex = queue.pop(0)
        # graph[vertex] 找到 vertex 的所有邻边（邻接点）
        nodes = graph[vertex]
        for w in nodes:
            if w not in seen:
                queue.append(w)
                seen.add(w)
                parent[w] = vertex
        # print(vertex)
    return parent

parent = BFS(graph, "A")
# for key in parent:
#     print(key, parent[key])
v = 'E'
while v != None:
    print(v)
    v = parent[v]