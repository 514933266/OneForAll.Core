# OneForAll.Core 核心类库
### 1. 扩展方法
```C#
// 填充字符串
str.Fmt(params object[] args) 

// 追加字符串
str.Append(string appendValue)

// 转换json对象，此处引用Newtonjson
str.ToJson<T>()
str.FromJson<T>()

// 全半角转换
str.DBCToSBC()
str.SBCToDBC()

// 是否为空
str.IsNullOrEmpty()
str.IsNullOrWhiteSpace()

// 正则
str.HasChinese()
str.IsEmail()

// 格式转换
str.TryInt()
str.TryBoolean() 
```
### 2. 加密解密
```C#
AESHelper.Encrypt(string input, string key)
AESHelper.Decrypt(string input, string key)
Base64Helper.Encrypt(string input, Encoding encode = null)
Base64Helper.Decrypt(string input, Encoding encode = null)
DESHelper.Encrypt(string input, string encryptKey)
DESHelper.Decrypt(string input, string decryptKey)
Md5Helper.Encrypt(string input, Encoding encoding = null)

str.ToAES(string key)
str.FromAES(string key)
str.ToBase64()
str.FromBase64()
str.ToMd5()
```
### 3. XML序列化
```C#
// 把xml序列化为字符串
SerializationHelper.SerializeXml(object obj, Encoding encoding)

// 将一个对象按XML序列化的方式写入到一个文件
SerializationHelper.SerializeXmlToFile(object obj, string path, Encoding encoding)

// 从XML字符串中反序列化对象
SerializationHelper.DeserializeXml<T>(string xmlString, Encoding encoding)
```
### 4. 字符串
```C#
// 获取随机字符串
StringHelper.GetRandomNumber(int length)

// 随机生成编号
StringHelper.GetRandomNumber(string PreStr, int lenght = 4)

// 获取随机字符串
StringHelper.GetRandomString(int length)

// 获取随机字符串
StringHelper.GetRandomLetter(int length)

// 替换特殊符号为空(仅剩数字和英文字母)
StringHelper.RemoveSymbol(string value)

// 将指定的字符串集合替换指定的字符串
StringHelper.RemoveSymbol(string value, string[] symbols, string replacStr = "")

// 截取某段字符串
StringHelper.MatchMiddleValue(string value, string begin, string end)
```
### 5. 时间
```C#
// 转换时间戳
TimeHelper.ToTimeStamp()
TimeHelper.ToTimeStamp(DateTime datetime)
TimeHelper.ToDateTime(long unix)

// 日期
TimeHelper.ToFirstDate(DateTime? date)
TimeHelper.ToLastDate(DateTime? date)
```
### 6. 通用对象
#### BaseErrType为常用的系统错误类型，示例
```C#
BaseErrType.Success
BaseErrType.Fail
BaseErrType.DataExist
```
#### BaseMessage为常用消息对象，里面包含BaseErrType返回状态码，也包含默认的消息方法，示例
```C#
new BaseMessage().Success(string msg = "success")
new BaseMessage().Fail(string msg = "fail")
new BaseMessage().Fail(BaseErrType type, string msg = "fail")
```
#### PageList为分页对象
```C#
new PageList(int total, int pageIndex, int pageSize, IEnumerable<T> items)
```

#### 7. 数组扩展
#### CollectioHelper为常用数组操作方法，可引用CollectionExtension使用扩展方法，示例
```C#
var tree = list.ToTree<Tree, int>();
var item = list.FindNode(nodeId);
var parent =list.FindParent(nodeId, parentId);
var children = list.FindChildren<Tree, int>(parent, true);
```