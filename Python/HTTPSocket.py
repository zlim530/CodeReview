import socket

HOST,PORT = '',23333

def server_run():
    listen_socket = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
    listen_socket.setsockopt(socket.SOL_SOCKET,socket.SO_REUSEADDR, 1 )
    listen_socket.bind((HOST,PORT))
    listen_socket.listen(1)
    print('Sering HTTP on port %s ...' % PORT)
    while True:
        client_connection,client_address = listen_sockect.accept()
        handle_request(client_connection)

def handle_request(client_connection):
    request = ''
    while True:
        recv_data = client_connection.recv(2400)
        recv_data = recv_data.decode()
        request += recv_data
        if len(recv_data) < 2400:
            break
        first_line_array = request.split('\r\n')[0].split('')
        space_line_index = request.index('\r\n\r\n')
        header = request[0:space_line_index]
        body = request[space_line_index+4:]

        print(request)

        http_response = b"""\
HTTP/1.1 200 OK

<!DOCTYPE html>
<html>
<head>
    <title>Hello, World!</title>
</head>
<body>
<p style="color: green">Hello, World!</p>
</body>
</html>
"""
        client_connection.sendall(http_request)
        client_connection.close()

if __name__ == '__main__':
    server_run()
