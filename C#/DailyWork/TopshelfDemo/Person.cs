using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace TopshelfDemo
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Person
    {
        public int Age { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        // 不需要是否结婚属性
        // [JsonIgnore]
        [JsonConverter(typeof(BoolConvert))]
        public bool IsMarry { get; set; }
        
        [JsonConverter(typeof(ChinaDatetimeConverter))]
        public DateTime Birthday { get; set; }
    }
    
    public class ChinaDatetimeConverter:DateTimeConverterBase
    {
        private static IsoDateTimeConverter _dateTimeConverter = new IsoDateTimeConverter(){ DateTimeFormat = "yyyy-MM-dd"};
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            _dateTimeConverter.WriteJson(writer,value,serializer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return _dateTimeConverter.ReadJson(reader, objectType, existingValue, serializer);
        }
    }

    public class LimitPropsContractResolver : DefaultContractResolver
    {
        private string[] props = null;
        private bool retain;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="props">传入的属性数组</param>
        /// <param name="retain">true：表示props是需要保留的字段；false：表示props是要排除的字段</param>
        public LimitPropsContractResolver(string[] props, bool retain = true)
        {
            this.props = props;
            this.retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            return list.Where(p =>
            {
                if (retain)
                {
                    return props.Contains(p.PropertyName);
                }
                else
                {
                    return !props.Contains(p.PropertyName);
                }

            }).ToList();
        }
    }

    public enum NotifyType
    {
        /// <summary>
        /// Email 发送
        /// </summary>
        Mail = 0,
        
        /// <summary>
        /// 短信发送
        /// </summary>
        SMS = 1
    }

    public class TestEnum
    {
        /// <summary>
        /// 消息发送类型
        /// </summary>
        /// 在Type属性上加上了JsonConverter(typeof(StringEnumConverter))表示将枚举值转换成对应的字符串,而StringEnumConverter是Newtonsoft.Json内置的转换类型,最终输出结果
        [JsonConverter(typeof(StringEnumConverter))]
        public NotifyType Type { get; set; }
    }

    /// <summary>
    /// 自定义了BoolConvert类型，继承自JsonConverter。构造函数参数BooleanString可以让我们自定义将true false转换成相应字符串
    /// </summary>
    public class BoolConvert:JsonConverter
    {
        private string[] arrBString { get; set; }

        public BoolConvert()
        {
            arrBString = "是,否".Split(',');
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="BooleanString">将bool值转换成的字符串值</param>
        public BoolConvert(string BooleanString)
        {
            if (string.IsNullOrEmpty(BooleanString))
            {
                throw new ArgumentNullException();
            }

            arrBString = BooleanString.Split(',');
            if (arrBString.Length != 2)
            {
                throw new ArgumentException("BooleanString格式不符合规定！");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            bool bValue = (bool) value;
            if (bValue)
            {
                writer.WriteValue(arrBString[0]);
            }
            else
            {
                writer.WriteValue(arrBString[1]);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = IsNullableType(objectType);
            Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;
            if (reader.TokenType == JsonToken.Null)
            {
                if (!IsNullableType(objectType))
                {
                    throw new Exception(string.Format("不能转换null value to {0}.",objectType));
                }

                return null;
            }
            try
            {
                if (reader.TokenType == JsonToken.String)
                {
                    var boolText = reader.Value.ToString();
                    if (boolText.Equals(arrBString[0],StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else if (boolText.Equals(arrBString[1],StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }

                if (reader.TokenType == JsonToken.Integer)
                {
                    return Convert.ToInt32(reader.Value) == 1;
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Error converting value {0} to type '{1}'", reader.Value,
                    objectType));
            }
            throw new Exception(string.Format("Unexpected token {0} when parsing enum",reader.TokenType));
        }

        private bool IsNullableType(Type t)
        {
            if (t == null)
            {
                throw new ArgumentException("t");
            }

            return t.BaseType.FullName == "System.ValueType" && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 判断是否为`Bool`类型
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
    
}














