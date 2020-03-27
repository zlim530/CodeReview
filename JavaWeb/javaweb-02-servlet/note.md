#### web.xml

```
<?xml version="1.0" encoding="UTF-8"?>
   <web-app xmlns="http://xmlns.jcp.org/xml/ns/javaee"
            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
            xsi:schemaLocation="http://xmlns.jcp.org/xml/ns/javaee
                         http://xmlns.jcp.org/xml/ns/javaee/web-app_4_0.xsd"
            version="4.0"
            metadata-complete="true">
       
   </web-app>
```
#### 关于在Maven项目中创建webapp骨架并使用@WebServlet注解实现Servlet路径配置问题

       1. 修改web.xml为4.0版本：参考上面，并将metadata-complete属性设为false
       2. 在Maven主工程的pom.xml文件的依赖中使用版本大于3的servlet jar包
       3. 并在pom.xml文件中添加provided标签：即<scope>provided</scope>