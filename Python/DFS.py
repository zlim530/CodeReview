graph = {
    "A": ["B","C"],
    "B": ["A","C","D"],
    "C": ["A","B","D","E"],
    "D": ["B","C","E","F"],
    "E": ["C","D"],
    "F": ["D"],
}

def DFS(graph, s):
    stack = []
    stack.append(s)
    # 已经访问过的节点
    seen = set()
    seen.add(s)
    while(len(stack) > 0):
        vertex = stack.pop()
        # graph[vertex] 找到 vertex 的所有邻边（邻接点）
        nodes = graph[vertex]
        for w in nodes:
            if w not in seen:
                stack.append(w)
                seen.add(w)
        print(vertex)

DFS(graph, "A")