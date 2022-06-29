print("pls input your height:")
height = input()
print("pls input your weight:")
weight = input()
bmi = round(float(weight) / (float(height) ** 2), 2)
if bmi < 18.5:
    print(f'bmi 为：{bmi}，bmi 小于18.5 => 过轻')
elif bmi >= 18.5 and bmi <= 25:
    print(f"bmi 为：{bmi}，bmi 在18.5与25之间 => 正常")
elif bmi > 25 and bmi <= 28:
    print(f"bmi 为：{bmi}，bmi 大于25小于28 => 过重")
elif bmi > 28 and bmi <= 32:
    print(f"bmi 为：{bmi}，bmi 大于28小于32 => 肥胖")
else: 
    print(f"bmi 为：{bmi}，bmi 大于32 => 严重肥胖")

# python 为弱类型语言：即对象有类型，变量没有类型，因此 Python 变量的类型永远跟着对象实例的类型走，所以在 Python 中重写父类的方法，也没有多态的效果
class Vehicle:
    def run(self):
        print("I'm running!")

class Car(Vehicle):
    def run(self):
        print("Car is running!")

class RaseCar(Car):
    def run(self):
        print("Race car is running!")

v = Vehicle()
v.run()
v = Car()
v.run()
v = RaseCar()
v.run()