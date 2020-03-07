package com.zlim;

import org.junit.Test;

import java.lang.annotation.ElementType;
import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.Method;

/**
 *
 *  关于java.lang.Class类的理解
 *  1.类的加载过程：
 *  程序经过javac.exe命令以后，会生成一个或多个字节码文件(以.class结尾)。---> 编译过程
 *  运行/加载过程:
 *  接着我们使用java.exe命令对某个字节码文件进行解释运行。(与即执行有main方法的那个类)相当于将某个字节码文件
 *  加载到内存中。此过程就称为类的加载。加载到内存中的类，我们就称为运行时类，此
 *  运行时类(即你在内存中出现了你就是运行时类)，就作为Class的一个实例。
 *  即类本身也是一个对象,它是Class类的对象.
 *
 *  故在Java中:万事万物皆对象:对象\File\URL\反射\前端\数据库操作等...
 *
 *  2.换句话说，Class的实例就对应着一个运行时类。故千万不要new一个Class实例,Class实例是运行时类
 *  3.加载到内存中的运行时类，会缓存一定的时间。在此时间之内，我们可以通过不同的方式
 *  来获取此运行时类。
 *
 *
 * @author zlim
 * @create 2020-03-07 19:22
 */
public class ReflectionTest {
    
    @Test
    public void test() throws Exception {
        // 反射的源头:Class类
        // Class也是泛型的，但是可以不写，如果写上则在后面获得相应运行类的实例对象时不需要再进行强制转换
        Class/*<Person>*/ clazz = Person.class;
        Constructor cons = clazz.getDeclaredConstructor(String.class, int.class);
        Object o = cons.newInstance("Tom", 22);
        Person p = (Person) o;

        System.out.println( p.toString());

        //2.通过反射，调用对象指定的属性、方法
        //调用属性  getDeclaredField(属性名);
        Field age = clazz.getDeclaredField("age");
        System.out.println(p.toString());
        age.set(p,23);
        System.out.println(p.toString());

        //调用方法 ： getDeclaredMethod("方法名",形参类型1.class,形参类型2.class....)：调用了Person类中无形参的show方法
        Method show = clazz.getDeclaredMethod("show");
        show.invoke(p);

        System.out.println("************************************");

        //通过反射，可以调用Person类的私有结构的。比如：私有的构造器、方法、属性
        //调用私有的构造器：getDeclaredConstructor(形参类型1.class,形参类型2.class....)
        Constructor con1 = clazz.getDeclaredConstructor(String.class);
        con1.setAccessible(true);
        Person p2 = (Person) con1.newInstance("TomV2");
        System.out.println("p2 = " + p2.toString());
        System.out.println("p = " + p.toString());

        // 调用私有的属性
        Field name = clazz.getDeclaredField("name");
        name.setAccessible(true);
        name.set(p2,"Jerry");
        System.out.println(p2.toString());

        //调用私有的方法
        // 第二个参数表示showNation方法需要一个形参，形参类型为String
        Method showNation = clazz.getDeclaredMethod("showNation", String.class);
        showNation.setAccessible(true);
        // invoke(运行类实例对象,调用方法的所需的实参...)
        String nation = (String) showNation.invoke(p2, "Chinese");
        System.out.println("nation = " + nation);


    }

    //疑问1：通过直接new的方式或反射的方式都可以调用公共的结构，开发中到底用那个？
    //建议：直接new的方式。
    //什么时候会使用：反射的方式。 反射的特征：动态性:即在编译时无法确定到底应该新建哪个类的对象时需要使用反射
    //疑问2：反射机制与面向对象中的封装性是不是矛盾的？如何看待两个技术？
    // 例如单例模式中将类的构造器私有化,无法在外面创建对象,但是现在通过反射则可以在外面创建对象
    //不矛盾。 封装性:告诉我们私有的方法不要去用,建议我们去调用公有的方法,因为有可能你调用了私有方法后跟类中提供的公共方法执行的逻辑是一样
    //              那么你不可没必要再去自己写一遍这个逻辑操作,直接用我提供给你的公共方法就好了
    //              例如单例模式中的私有方法:它告诉我们如果想新建对象不要去(也不能去)调用私有的构造器
    //              我已经在这个类的内部帮你创建好了这个类的对象,你通过我编写好的方法进行对象的获取就行了
    //        反射: 而反射解决的是能不能访问的问题




    //获取Class的实例的方式（前三种方式需要掌握）:四种方法
    @Test
    public void testGetClass() throws ClassNotFoundException {
        //方式一：调用运行时类的属性：.class
        // Class<Person> clazz = Person.class; // 其实Class也是有泛型的,这样写了之后后面获取实例时就不需要进行类型转换了
        // 但是不写也是可以的,即表明为Object类
        Class<Person> perclass = Person.class;
        System.out.println("perclass = " + perclass);// perclass = class com.zlim.Person

        //方式二：通过运行时类的对象,通过运行时类的对象调用getClass()
        Person person = new Person("Ll",23);
        Class<? extends Person> perclass2 = person.getClass();
        System.out.println("perclass2 = " + perclass2);

        //方式三：调用Class的静态方法：forName(String classPath):参数为类的全类名(即包含包名的全路径)
        // 此静态方法会抛出ClassNotFoundException异常
        // 这种方式用的比较多：因为这个异常是运行时异常而不是编译时异常：只有当我们运行程序时才知道这个类是否存在
        Class<?> perclass3 = Class.forName("com.zlim.Person");
        System.out.println("perclass3 = " + perclass3);

        // 判断地址值:三个Class对象都指向了内存中同一个地址值
        // 尽管我们通过了不同的方法获取了三个Class类对象,但其实这三个对象都指向了同一个地址,也即其实都是同一个运行时类对象(唯一存在的)
        System.out.println(perclass == perclass2);  // true
        System.out.println(perclass == perclass3);  // true

        //方式四：使用类的加载器：ClassLoader  (了解)
        ClassLoader classLoader = ReflectionTest.class.getClassLoader();
        Class<?> perclass4 = classLoader.loadClass("com.zlim.Person");
        System.out.println("perclass4 = " + perclass4); // true
        System.out.println(perclass == perclass4);

    }



    //万事万物皆对象？对象.xxx,File,URL,反射,前端、数据库操作
    //Class实例可以是哪些结构的说明：
    @Test
    public void testClass(){
        Class<Object> objectClass = Object.class;
        Class<Comparable> comparableClass = Comparable.class;
        Class<String[]> aClass = String[].class;
        Class<int[][]> aClass1 = int[][].class;
        Class<ElementType> elementTypeClass = ElementType.class;
        // Override：注解
        Class<Override> overrideClass = Override.class;
        Class<Integer> integerClass = int.class;
        Class<Void> voidClass = void.class;
        Class<Class> classClass = Class.class;

        int[] a = new  int[10];
        int[] b = new  int[100];
        Class<? extends int[]> ac = a.getClass();
        Class<? extends int[]> bc = b.getClass();
        // 只要数组的元素类型与维度一样，就是同一个Class
        System.out.println(ac == bc);   // true：在这里a和b都是一维整型数组
    }








}
