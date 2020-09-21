using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RestApisGen
{
    public class ApiEndpoint
    {
        private static readonly string[] valueTypes = { "int", "long", "byte", "double", "bool", "DateTimeOffset", "UploadMediaType", "TweetMode", "Bucket", "Format", "TweetExpansions", "UserExpansions", "MediaFields", "PlaceFields", "PollFields", "TweetFields", "UserFields" };

        public string Name { get; set; }

        public string Request { get; set; }

        public string ReturnType { get; set; }

        public ApiType Type { get; set; }

        public IEnumerable<string> ReservedNames { get; set; }

        public string JsonPath { get; set; }

        public string[] JsonMap { get; set; }

        public Tuple<string, string>[] Attributes { get; set; }

        public CursorMode CursorMode { get; set; }

        public string CursorElementType { get; set; }

        public string Uri { get; set; }

        public string[] Description { get; set; }

        public string Returns { get; set; }

        public string UrlPrefix { get; set; }

        public string UrlSuffix { get; set; }

        public Parameter[] Params = new Parameter[0];

        public string[][] CustomBodies = new string[8][];

        public string[] OmitExcept = new string[0];

        public List<List<Parameter[]>> AnyOneGroups = new List<List<Parameter[]>>();

        public Dictionary<string, string> MethodCondition { get; } = new Dictionary<string, string>()
        {
            { "pe", "SYNC" },
            { "id", "SYNC" },
            { "t", "SYNC" },
            { "enumerate", "SYNC" },
            { "static", "SYNC" },
            { "asyncpe", "ASYNC" },
            { "asyncid", "ASYNC" },
            { "asynct", "ASYNC" },
            { "asyncstatic", "ASYNC" },
        };

        public string MethodDefinition
        {
            get
            {
                switch (this.Type)
                {
                    case ApiType.Void:
                    case ApiType.Normal:
                        return string.Format("public {0} {1}", this.ReturnType, this.Name);
                    case ApiType.Listed:
                        return string.Format("public ListedResponse<{0}> {1}", this.ReturnType, this.Name);
                    case ApiType.Dictionary:
                        return string.Format("public DictionaryResponse<string, {0}> {1}", this.ReturnType, this.Name);
                    default:
                        throw new ArgumentException("");
                }
            }
        }

        public string MethodDefinitionAsync
        {
            get
            {
                switch (this.Type)
                {
                    case ApiType.Void:
                        return string.Format("public Task {0}Async", this.Name);
                    case ApiType.Normal:
                        return string.Format("public Task<{0}> {1}Async", this.ReturnType, this.Name);
                    case ApiType.Listed:
                        return string.Format("public Task<ListedResponse<{0}>> {1}Async", this.ReturnType, this.Name);
                    case ApiType.Dictionary:
                        return string.Format("public Task<DictionaryResponse<string, {0}>> {1}Async", this.ReturnType, this.Name);
                    default:
                        throw new ArgumentException("");
                }
            }
        }

        public IEnumerable<Parameter> SpecialKeyParameters => Params.Where(x => x.ParameterName != $"\"{x.RealName}\"");
        public string DictionaryKeyTransformerOrEmpty => SpecialKeyParameters.Any() ? $".Select(x => new KeyValuePair<string, object>({SpecialKeyParameters.Aggregate("", (a, c) => a + $"x.Key == \"{c.RealName}\" ? {c.ParameterName} : ")}x.Key, x.Value))" : "";

        public string JsonPathOrEmpty => JsonPath != null ? $", jsonPath: \"{JsonPath}\"" : "";

        public string UrlPrefixOrNullString => UrlPrefix ?? "null";
        public string UrlSuffixOrNullString => UrlSuffix ?? "null";
        public string CustomBaseUrlOrEmpty =>
            (UrlPrefix != null ? $", urlPrefix: {UrlPrefix}" : "") +
            (UrlSuffix != null ? $", urlSuffix: {UrlSuffix}" : "");

        string FormatWith(int i, string s, params object[] args)
        {
            if (CustomBodies[i] != null)
                return string.Join(Environment.NewLine, CustomBodies[i]);
            else
                return string.Format(s, args);
        }

        private bool CustomImpl => this.Request == "Impl";

        internal string[] jsonMapVar
        {
            get
            {
                var l = new List<string>();
                l.Add("var jm = new string[" + JsonMap.Length + "];");
                l.AddRange(
                    JsonMap.Select((x, i) =>
                        string.Format("jm[{0}] = \"{1}\";", i, x.Replace("\"", "\\\""))
                    )
                );
                return l.ToArray();
            }
        }

        public Method PE
        {
            get
            {
                var s1 = this.MethodDefinition + "(params Expression<Func<string, object>>[] parameters)";
                var s2 = "";
                var ls = new List<string>();
                if (this.ReservedNames != null)
                {
                    switch (this.Type)
                    {
                        case ApiType.Void:
                            s2 = FormatWith(0,
                                "this.Tokens.AccessParameterReservedApiNoResponse(MethodType.{0}, \"{1}\", new [] {{ \"{2}\" }}, InternalUtils.ExpressionsToDictionary(parameters){3}{4});",
                                this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), DictionaryKeyTransformerOrEmpty, CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Normal:
                            s2 = FormatWith(0,
                                "return this.Tokens.AccessParameterReservedApi<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, InternalUtils.ExpressionsToDictionary(parameters){4}{5});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), DictionaryKeyTransformerOrEmpty, CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Listed:
                            s2 = FormatWith(0,
                                "return this.Tokens.AccessParameterReservedApiArray<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, InternalUtils.ExpressionsToDictionary(parameters){4}{5});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), DictionaryKeyTransformerOrEmpty, CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (this.JsonMap != null)
                        ls.AddRange(jsonMapVar);

                    if (this.CustomImpl)
                    {
                        var callImpl = this.JsonMap == null
                            ? "this.{0}Impl(InternalUtils.ExpressionsToDictionary(parameters){1}, {2}, {3});"
                            : "this.{0}Impl(InternalUtils.ExpressionsToDictionary(parameters){1}, jm, {2}, {3});";
                        s2 = FormatWith(0, this.Type == ApiType.Void
                            ? callImpl
                            : "return " + callImpl,
                            this.Name, this.DictionaryKeyTransformerOrEmpty, this.UrlPrefixOrNullString, this.UrlSuffixOrNullString);
                    }
                    else if(this.JsonMap != null)
                    {
                        switch (this.Type)
                        {
                            case ApiType.Normal:
                                s2 = FormatWith(0, "return this.Tokens.AccessJsonParameteredApi<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(0, "return this.Tokens.AccessJsonParameteredApiArray<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        switch (this.Type)
                        {
                            case ApiType.Void:
                                s2 = FormatWith(0, "this.Tokens.AccessApiNoResponse(MethodType.{0}, \"{1}\", parameters{2});", this.Request, this.Uri, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Normal:
                                s2 = FormatWith(0, "return this.Tokens.AccessApi<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(0, "return this.Tokens.AccessApiArray<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Dictionary:
                                s2 = FormatWith(0, "return this.Tokens.AccessApiDictionary<string, {0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                }
                ls.Add(s2);
                return new Method(s1, this.Params, ls.ToArray(), this.MethodCondition["pe"]);
            }
        }

        public Method ID
        {
            get
            {
                var s1 = this.MethodDefinition + "(IDictionary<string, object> parameters)";
                var s2 = "";
                var ls = new List<string>();
                if (this.ReservedNames != null)
                {
                    switch (this.Type)
                    {
                        case ApiType.Void:
                            s2 = FormatWith(1,
                                "this.Tokens.AccessParameterReservedApiNoResponse(MethodType.{0}, \"{1}\", new [] {{ \"{2}\" }}, parameters{3});"
                                , this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Normal:
                            s2 = FormatWith(1,
                                "return this.Tokens.AccessParameterReservedApi<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, parameters{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Listed:
                            s2 = FormatWith(1,
                                "return this.Tokens.AccessParameterReservedApiArray<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, parameters{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (this.JsonMap != null)
                        ls.AddRange(jsonMapVar);

                    if (this.CustomImpl)
                    {
                        var callImpl = this.JsonMap == null
                            ? "this.{0}Impl(parameters, {1}, {2});"
                            : "this.{0}Impl(parameters, jm, {1}, {2});";
                        s2 = FormatWith(1, this.Type == ApiType.Void
                            ? callImpl
                            : "return " + callImpl,
                            this.Name, this.UrlPrefixOrNullString, this.UrlSuffixOrNullString);
                    }
                    else if(this.JsonMap != null)
                    {
                        switch (this.Type)
                        {
                            case ApiType.Normal:
                                s2 = FormatWith(1, "return this.Tokens.AccessJsonParameteredApi<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(1, "return this.Tokens.AccessJsonParameteredApiArray<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        switch (this.Type)
                        {
                            case ApiType.Void:
                                s2 = FormatWith(1, "this.Tokens.AccessApiNoResponse(MethodType.{0}, \"{1}\", parameters{2});", this.Request, this.Uri, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Normal:
                                s2 = FormatWith(1, "return this.Tokens.AccessApi<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(1, "return this.Tokens.AccessApiArray<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Dictionary:
                                s2 = FormatWith(1, "return this.Tokens.AccessApiDictionary<string, {0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                }
                ls.Add(s2);
                return new Method(s1, this.Params, ls.ToArray(), this.MethodCondition["id"]);
            }
        }

        public Method T
        {
            get
            {
                var s1 = this.MethodDefinition + "(object parameters)";
                var s2 = "";
                var ls = new List<string>();
                if (this.ReservedNames != null)
                {
                    switch (this.Type)
                    {
                        case ApiType.Void:
                            s2 = FormatWith(2,
                                "this.Tokens.AccessParameterReservedApiNoResponse(MethodType.{0}, \"{1}\", new [] {{ \"{2}\" }}, InternalUtils.ResolveObject(parameters){3});"
                                , this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Normal:
                            s2 = FormatWith(2,
                                "return this.Tokens.AccessParameterReservedApi<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, InternalUtils.ResolveObject(parameters){4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Listed:
                            s2 = FormatWith(2,
                                "return this.Tokens.AccessParameterReservedApiArray<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, InternalUtils.ResolveObject(parameters){4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (this.JsonMap != null)
                        ls.AddRange(jsonMapVar);

                    if (this.CustomImpl)
                    {
                        var callImpl = this.JsonMap == null
                            ? "this.{0}Impl(InternalUtils.ResolveObject(parameters), {1}, {2});"
                            : "this.{0}Impl(InternalUtils.ResolveObject(parameters), jm, {1}, {2});";
                        s2 = FormatWith(2, this.Type == ApiType.Void
                            ? callImpl
                            : "return " + callImpl,
                            this.Name, this.UrlPrefixOrNullString, this.UrlSuffixOrNullString);
                    }
                    else if(this.JsonMap != null)
                    {
                        switch (this.Type)
                        {
                            case ApiType.Normal:
                                s2 = FormatWith(2, "return this.Tokens.AccessJsonParameteredApi<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(2, "return this.Tokens.AccessJsonParameteredApiArray<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        switch (this.Type)
                        {
                            case ApiType.Void:
                                s2 = FormatWith(2, "this.Tokens.AccessApiNoResponse(MethodType.{0}, \"{1}\", parameters{2});", this.Request, this.Uri, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Normal:
                                s2 = FormatWith(2, "return this.Tokens.AccessApi<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(2, "return this.Tokens.AccessApiArray<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Dictionary:
                                s2 = FormatWith(2, "return this.Tokens.AccessApiDictionary<string, {0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                }
                ls.Add(s2);
                return new Method(s1, this.Params, ls.ToArray(), this.MethodCondition["t"]);
            }
        }

        public Method[] Static
        {
            get
            {
                var eithered = Extensions.Combinate(this.AnyOneGroups);
                var uneithered = this.Params.Where(x => !AnyOneGroups.SelectMany(_ => _).SelectMany(_ => _).Contains(x));
                var rs = new List<Method>();
                var whenClause = this.MethodCondition["static"];

                var s2 = "";
                var ls = new List<string>();
                if (this.ReservedNames != null)
                {
                    switch (this.Type)
                    {
                        case ApiType.Void:
                            s2 = FormatWith(3,
                                "this.Tokens.AccessParameterReservedApiNoResponse(MethodType.{0}, \"{1}\", new [] {{ \"{2}\" }}, parameters{3});"
                                , this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Normal:
                            s2 = FormatWith(3,
                                "return this.Tokens.AccessParameterReservedApi<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, parameters{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Listed:
                            s2 = FormatWith(3,
                                "return this.Tokens.AccessParameterReservedApiArray<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, parameters{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (this.JsonMap != null)
                        ls.AddRange(jsonMapVar);

                    if (this.CustomImpl)
                    {
                        var callImpl = this.JsonMap == null
                            ? "this.{0}Impl(parameters, {1}, {2});"
                            : "this.{0}Impl(parameters, jm, {1}, {2});";
                        s2 = FormatWith(3, this.Type == ApiType.Void
                            ? callImpl
                            : "return " + callImpl,
                            this.Name, this.UrlPrefixOrNullString, this.UrlSuffixOrNullString);
                    }
                    else if(this.JsonMap != null)
                    {
                        switch (this.Type)
                        {
                            case ApiType.Normal:
                                s2 = FormatWith(3, "return this.Tokens.AccessJsonParameteredApi<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(3, "return this.Tokens.AccessJsonParameteredApiArray<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        switch (this.Type)
                        {
                            case ApiType.Void:
                                s2 = FormatWith(3, "this.Tokens.AccessApiNoResponse(MethodType.{0}, \"{1}\", parameters{2});", this.Request, this.Uri, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Normal:
                                s2 = FormatWith(3, "return this.Tokens.AccessApi<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(3, "return this.Tokens.AccessApiArray<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Dictionary:
                                s2 = FormatWith(3, "return this.Tokens.AccessApiDictionary<string, {0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                }

                if (AnyOneGroups.Count != 0)
                    foreach (var x in eithered)
                    {
                        var o = this.Params.Where(y => !this.AnyOneGroups.SelectMany(_ => _).SelectMany(_ => _).Contains(y) || x.SelectMany(_ => _).Contains(y));
                        var s1 = this.MethodDefinition + "(" +
                            string.Join(", ",
                                o.Select(y =>
                                    (y.IsOptional && valueTypes.Contains(y.Type)
                                        ? y.Type + "?"
                                        : y.Type)
                                    + " " + y.Name + (y.IsOptional ? " = null" : ""))) + ")";
                        var prmps = new List<string>();
                        prmps.AddRange(ls);
                        prmps.Add("var parameters = new Dictionary<string, object>();");
                        foreach (var y in o)
                        {
                            if (y.IsOptional)
                                prmps.Add(string.Format("if({0} != null) parameters.Add({1}, {0});", y.Name, y.ParameterName));
                            else if (valueTypes.Contains(y.Type))
                                prmps.Add(string.Format("parameters.Add({1}, {0});", y.Name, y.ParameterName));
                            else
                            {
                                prmps.Add(string.Format("if({0} == null) throw new ArgumentNullException({1});", y.Name, y.ParameterName));
                                prmps.Add(string.Format("parameters.Add({1}, {0});", y.Name, y.ParameterName));
                            }
                        }
                        if (this.CursorMode != CursorMode.None)
                        {
                            string c2;
                            switch (this.CursorMode)
                            {
                                case CursorMode.Forward:
                                    c2 = string.Format("return Cursored.EnumerateForward<{0}, {1}>(this.Tokens, \"{2}\", parameters{3}{4});", this.ReturnType, this.CursorElementType, this.Uri, this.JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                    break;
                                case CursorMode.Both:
                                    c2 = string.Format("return Cursored.Enumerate<{0}>(this.Tokens, \"{1}\", mode, parameters{2}{3});", this.CursorElementType, this.Uri, this.JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                    break;
                                default:
                                    throw new NotImplementedException();
                            }

                            var name = "public IEnumerable<" + this.CursorElementType + "> Enumerate" + this.Name;
                            var c1 = name + (this.CursorMode == CursorMode.Both ? "(EnumerateMode mode, " : "(") +
                                string.Join(", ",
                                    o.Select(y =>
                                        (y.IsOptional && valueTypes.Contains(y.Type)
                                            ? y.Type + "?"
                                            : y.Type)
                                        + " " + y.Name + (y.IsOptional ? " = null" : ""))) + ")";
                            rs.Add(new Method(c1, o, prmps.Concat(new[] { c2 }).ToArray(), whenClause, true));
                        }
                        prmps.Add(s2);
                        rs.Add(new Method(s1, o, prmps.ToArray(), whenClause, true));
                    }
                else
                {
                    var s1 = this.MethodDefinition + "(" +
                        string.Join(", ",
                            uneithered.Select(y =>
                                (y.IsOptional && valueTypes.Contains(y.Type)
                                    ? y.Type + "?"
                                    : y.Type)
                                + " " + y.Name + (y.IsOptional ? " = null" : ""))) + ")";
                    var prmps = new List<string>();
                    prmps.AddRange(ls);
                    prmps.Add("var parameters = new Dictionary<string, object>();");

                    foreach (var y in uneithered)
                        if (y.IsOptional)
                            prmps.Add(string.Format("if({0} != null) parameters.Add({1}, {0});", y.Name, y.ParameterName));
                        else if (valueTypes.Contains(y.Type))
                            prmps.Add(string.Format("parameters.Add({1}, {0});", y.Name, y.ParameterName));
                        else
                        {
                            prmps.Add(string.Format("if({0} == null) throw new ArgumentNullException({1});", y.Name, y.ParameterName));
                            prmps.Add(string.Format("parameters.Add({1}, {0});", y.Name, y.ParameterName));
                        }

                    if (this.CursorMode != CursorMode.None)
                    {
                        string c2;
                        switch (this.CursorMode)
                        {
                            case CursorMode.Forward:
                                c2 = string.Format("return Cursored.EnumerateForward<{0}, {1}>(this.Tokens, \"{2}\", parameters{3}{4});", this.ReturnType, this.CursorElementType, this.Uri, this.JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case CursorMode.Both:
                                c2 = string.Format("return Cursored.Enumerate<{0}>(this.Tokens, \"{1}\", mode, parameters{2}{3});", this.CursorElementType, this.Uri, this.JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }

                        var name = "public IEnumerable<" + this.CursorElementType + "> Enumerate" + this.Name;
                        var c1 = name + (this.CursorMode == CursorMode.Both ? "(EnumerateMode mode, " : "(") +
                            string.Join(", ",
                                uneithered.Select(y =>
                                    (y.IsOptional && valueTypes.Contains(y.Type)
                                        ? y.Type + "?"
                                        : y.Type)
                                    + " " + y.Name + (y.IsOptional ? " = null" : ""))) + ")";
                        rs.Add(new Method(c1, this.Params, prmps.Concat(new[] { c2 }).ToArray(), whenClause, true));
                    }
                    prmps.Add(s2);
                    rs.Add(new Method(s1, this.Params, prmps.ToArray(), whenClause, true));
                }
                return rs.ToArray();
            }
        }

        public Method PEAsync
        {
            get
            {
                var s1 = this.MethodDefinitionAsync + "(params Expression<Func<string, object>>[] parameters)";
                var s2 = "";
                var ls = new List<string>();
                if (this.ReservedNames != null)
                {
                    switch (this.Type)
                    {
                        case ApiType.Void:
                            s2 = FormatWith(4,
                                "return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.{0}, \"{1}\", new [] {{ \"{2}\" }}, InternalUtils.ExpressionsToDictionary(parameters){3}, CancellationToken.None{4});"
                                , this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), DictionaryKeyTransformerOrEmpty, CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Normal:
                            s2 = FormatWith(4,
                                "return this.Tokens.AccessParameterReservedApiAsync<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, InternalUtils.ExpressionsToDictionary(parameters){4}, CancellationToken.None{5});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), DictionaryKeyTransformerOrEmpty, CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Listed:
                            s2 = FormatWith(4,
                                "return this.Tokens.AccessParameterReservedApiArrayAsync<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, InternalUtils.ExpressionsToDictionary(parameters){4}, CancellationToken.None{5});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), DictionaryKeyTransformerOrEmpty, CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (this.JsonMap != null)
                        ls.AddRange(jsonMapVar);

                    if (this.CustomImpl)
                    {
                        s2 = FormatWith(4,
                            this.JsonMap == null
                                ? "return this.{0}AsyncImpl(InternalUtils.ExpressionsToDictionary(parameters){1}, CancellationToken.None, {2}, {3});"
                                : "return this.{0}AsyncImpl(InternalUtils.ExpressionsToDictionary(parameters){1}, jm, CancellationToken.None, {2}, {3});",
                            this.Name, this.DictionaryKeyTransformerOrEmpty, this.UrlPrefixOrNullString, this.UrlSuffixOrNullString);
                    }
                    else if (this.JsonMap != null)
                    {
                        switch (this.Type)
                        {
                            case ApiType.Normal:
                                s2 = FormatWith(4, "return this.Tokens.AccessJsonParameteredApiAsync<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(4, "return this.Tokens.AccessJsonParameteredApiArrayAsync<{0}>(\"{1}\", parameters, jm{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                        switch (this.Type)
                        {
                            case ApiType.Void:
                                s2 = FormatWith(4, "return this.Tokens.AccessApiNoResponseAsync(MethodType.{0}, \"{1}\", parameters{2});", this.Request, this.Uri, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Normal:
                                s2 = FormatWith(4, "return this.Tokens.AccessApiAsync<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(4, "return this.Tokens.AccessApiArrayAsync<{0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Dictionary:
                                s2 = FormatWith(4, "return this.Tokens.AccessApiDictionaryAsync<string, {0}>(MethodType.{1}, \"{2}\", parameters{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                }
                ls.Add(s2);
                return new Method(s1, this.Params, ls.ToArray(), this.MethodCondition["asyncpe"]);
            }
        }

        public Method IDAsync
        {
            get
            {
                var s1 = this.MethodDefinitionAsync + "(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))";
                var s2 = "";
                var ls = new List<string>();
                if (this.ReservedNames != null)
                {
                    switch (this.Type)
                    {
                        case ApiType.Void:
                            s2 = FormatWith(5,
                                "return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.{0}, \"{1}\", new [] {{ \"{2}\" }}, parameters, cancellationToken{3});"
                                , this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Normal:
                            s2 = FormatWith(5,
                                "return this.Tokens.AccessParameterReservedApiAsync<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, parameters, cancellationToken{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Listed:
                            s2 = FormatWith(5,
                                "return this.Tokens.AccessParameterReservedApiArrayAsync<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, parameters, cancellationToken{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (this.JsonMap != null)
                        ls.AddRange(jsonMapVar);

                    if (this.CustomImpl)
                    {
                        s2 = FormatWith(5,
                            this.JsonMap == null
                                ? "return this.{0}AsyncImpl(parameters, cancellationToken, {1}, {2});"
                                : "return this.{0}AsyncImpl(parameters, jm, cancellationToken, {1}, {2});",
                            this.Name, this.UrlPrefixOrNullString, this.UrlSuffixOrNullString);
                    }
                    else if (this.JsonMap != null)
                    {
                        switch (this.Type)
                        {
                            case ApiType.Normal:
                                s2 = FormatWith(5, "return this.Tokens.AccessJsonParameteredApiAsync<{0}>(\"{1}\", parameters, jm, cancellationToken{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(5, "return this.Tokens.AccessJsonParameteredApiArrayAsync<{0}>(\"{1}\", parameters, jm, cancellationToken{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                        switch (this.Type)
                        {
                            case ApiType.Void:
                                s2 = FormatWith(5, "return this.Tokens.AccessApiNoResponseAsync(MethodType.{0}, \"{1}\", parameters, cancellationToken{2});", this.Request, this.Uri, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Normal:
                                s2 = FormatWith(5, "return this.Tokens.AccessApiAsync<{0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(5, "return this.Tokens.AccessApiArrayAsync<{0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Dictionary:
                                s2 = FormatWith(5, "return this.Tokens.AccessApiDictionaryAsync<string, {0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                }
                ls.Add(s2);
                return new Method(s1, this.Params, ls.ToArray(), this.MethodCondition["asyncid"], takesCancellationToken: true);
            }
        }

        public Method TAsync
        {
            get
            {
                var s1 = this.MethodDefinitionAsync + "(object parameters, CancellationToken cancellationToken = default(CancellationToken))";
                var s2 = "";
                var ls = new List<string>();
                if (this.ReservedNames != null)
                {
                    switch (this.Type)
                    {
                        case ApiType.Void:
                            s2 = FormatWith(6,
                                "return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.{0}, \"{1}\", new [] {{ \"{2}\" }}, InternalUtils.ResolveObject(parameters), cancellationToken{3});"
                                , this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Normal:
                            s2 = FormatWith(6,
                                "return this.Tokens.AccessParameterReservedApiAsync<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, InternalUtils.ResolveObject(parameters), cancellationToken{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Listed:
                            s2 = FormatWith(6,
                                "return this.Tokens.AccessParameterReservedApiArrayAsync<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, InternalUtils.ResolveObject(parameters), cancellationToken{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (this.JsonMap != null)
                        ls.AddRange(jsonMapVar);

                    if (this.CustomImpl)
                    {
                        s2 = FormatWith(6,
                            this.JsonMap == null
                                ? "return this.{0}AsyncImpl(InternalUtils.ResolveObject(parameters), cancellationToken, {1}, {2});"
                                : "return this.{0}AsyncImpl(InternalUtils.ResolveObject(parameters), jm, cancellationToken, {1}, {2});",
                            this.Name, this.UrlPrefixOrNullString, this.UrlSuffixOrNullString);
                    }
                    else if (this.JsonMap != null)
                    {
                        switch (this.Type)
                        {
                            case ApiType.Normal:
                                s2 = FormatWith(6, "return this.Tokens.AccessJsonParameteredApiAsync<{0}>(\"{1}\", parameters, jm, cancellationToken{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(6, "return this.Tokens.AccessJsonParameteredApiArrayAsync<{0}>(\"{1}\", parameters, jm, cancellationToken{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                        switch (this.Type)
                        {
                            case ApiType.Void:
                                s2 = FormatWith(6, "return this.Tokens.AccessApiNoResponseAsync(MethodType.{0}, \"{1}\", parameters, cancellationToken{2});", this.Request, this.Uri, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Normal:
                                s2 = FormatWith(6, "return this.Tokens.AccessApiAsync<{0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(6, "return this.Tokens.AccessApiArrayAsync<{0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Dictionary:
                                s2 = FormatWith(6, "return this.Tokens.AccessApiDictionaryAsync<string, {0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                }
                ls.Add(s2);
                return new Method(s1, this.Params, ls.ToArray(), this.MethodCondition["asynct"], takesCancellationToken: true);
            }
        }

        public Method[] StaticAsync
        {
            get
            {
                var eithered = Extensions.Combinate(this.AnyOneGroups);
                var uneithered = this.Params.Where(x => !AnyOneGroups.SelectMany(_ => _).SelectMany(_ => _).Contains(x));
                var rs = new List<Method>();
                var whenClause = this.MethodCondition["asyncstatic"];

                var s2 = "";
                var ls = new List<string>();
                if (this.ReservedNames != null)
                {
                    switch (this.Type)
                    {
                        case ApiType.Void:
                            s2 = FormatWith(7,
                                "return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.{0}, \"{1}\", new [] {{ \"{2}\" }}, parameters, cancellationToken{3});"
                                , this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Normal:
                            s2 = FormatWith(7,
                                "return this.Tokens.AccessParameterReservedApiAsync<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, parameters, cancellationToken{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        case ApiType.Listed:
                            s2 = FormatWith(7,
                                "return this.Tokens.AccessParameterReservedApiArrayAsync<{0}>(MethodType.{1}, \"{2}\", new [] {{ \"{3}\" }}, parameters, cancellationToken{4});"
                                , this.ReturnType, this.Request, this.Uri, string.Join("\", \"", this.ReservedNames), CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (this.JsonMap != null)
                        ls.AddRange(jsonMapVar);

                    if (this.CustomImpl)
                    {
                        s2 = FormatWith(7,
                            this.JsonMap == null
                                ? "return this.{0}AsyncImpl(parameters, cancellationToken, {1}, {2});"
                                : "return this.{0}AsyncImpl(parameters, jm, cancellationToken, {1}, {2});",
                            this.Name, this.UrlPrefixOrNullString, this.UrlSuffixOrNullString);
                    }
                    else if (this.JsonMap != null)
                    {
                        switch (this.Type)
                        {
                            case ApiType.Normal:
                                s2 = FormatWith(7, "return this.Tokens.AccessJsonParameteredApiAsync<{0}>(\"{1}\", parameters, jm, cancellationToken{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(7, "return this.Tokens.AccessJsonParameteredApiArrayAsync<{0}>(\"{1}\", parameters, jm, cancellationToken{2}{3});", this.ReturnType, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                        switch (this.Type)
                        {
                            case ApiType.Void:
                                s2 = FormatWith(7, "return this.Tokens.AccessApiNoResponseAsync(MethodType.{0}, \"{1}\", parameters, cancellationToken{2});", this.Request, this.Uri, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Normal:
                                s2 = FormatWith(7, "return this.Tokens.AccessApiAsync<{0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Listed:
                                s2 = FormatWith(7, "return this.Tokens.AccessApiArrayAsync<{0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            case ApiType.Dictionary:
                                s2 = FormatWith(7, "return this.Tokens.AccessApiDictionaryAsync<string, {0}>(MethodType.{1}, \"{2}\", parameters, cancellationToken{3}{4});", this.ReturnType, this.Request, this.Uri, JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                }

                if (eithered.Count() != 0)
                    foreach (var x in eithered)
                    {
                        var o = this.Params.Where(y => !this.AnyOneGroups.SelectMany(_ => _).SelectMany(_ => _).Contains(y) || x.SelectMany(_ => _).Contains(y));
                        var s1 = this.MethodDefinitionAsync + "(" +
                            string.Join(", ",
                                o.Select(y =>
                                    (y.IsOptional && valueTypes.Contains(y.Type)
                                        ? y.Type + "?"
                                        : y.Type)
                                    + " " + y.Name + (y.IsOptional ? " = null" : ""))
                                .Concat(new[] { "CancellationToken cancellationToken = default(CancellationToken))" }));
                        var prmps = new List<string>();
                        prmps.AddRange(ls);
                        prmps.Add("var parameters = new Dictionary<string, object>();");
                        foreach (var y in o)
                        {
                            if (y.IsOptional)
                                prmps.Add(string.Format("if({0} != null) parameters.Add({1}, {0});", y.Name, y.ParameterName));
                            else if (valueTypes.Contains(y.Type))
                                prmps.Add(string.Format("parameters.Add({1}, {0});", y.Name, y.ParameterName));
                            else
                            {
                                prmps.Add(string.Format("if({0} == null) throw new ArgumentNullException({1});", y.Name, y.ParameterName));
                                prmps.Add(string.Format("parameters.Add({1}, {0});", y.Name, y.ParameterName));
                            }
                        }
                        prmps.Add(s2);
                        rs.Add(new Method(s1, o, prmps.ToArray(), whenClause, true, true));
                    }
                else
                {
                    var s1 = this.MethodDefinitionAsync + "(" +
                        string.Join(", ",
                            uneithered.Select(y =>
                                (y.IsOptional && valueTypes.Contains(y.Type)
                                    ? y.Type + "?"
                                    : y.Type)
                                + " " + y.Name + (y.IsOptional ? " = null" : ""))) + (uneithered.Count() != 0 ? ", " : "") + "CancellationToken cancellationToken = default(CancellationToken))";
                    var prmps = new List<string>();
                    prmps.AddRange(ls);
                    prmps.Add("var parameters = new Dictionary<string, object>();");

                    foreach (var y in uneithered)
                        if (y.IsOptional)
                            prmps.Add(string.Format("if({0} != null) parameters.Add({1}, {0});", y.Name, y.ParameterName));
                        else if (valueTypes.Contains(y.Type))
                            prmps.Add(string.Format("parameters.Add({1}, {0});", y.Name, y.ParameterName));
                        else
                        {
                            prmps.Add(string.Format("if({0} == null) throw new ArgumentNullException({1});", y.Name, y.ParameterName));
                            prmps.Add(string.Format("parameters.Add({1}, {0});", y.Name, y.ParameterName));
                        }

                    prmps.Add(s2);
                    rs.Add(new Method(s1, uneithered, prmps.ToArray(), whenClause, true, true));
                }
                return rs.ToArray();
            }
        }

        public Dictionary<string, Method[]> MethodDic
        {
            get
            {
                var dic = new Dictionary<string, Method[]>();
                dic.Add("pe", new[] { this.PE });
                dic.Add("id", new[] { this.ID });
                dic.Add("t", new[] { this.T });
                dic.Add("static", this.Static);
                var l = new List<Method>();
                if (this.CursorMode != CursorMode.None)
                {
                    string returnLine;
                    switch (this.CursorMode)
                    {
                        case CursorMode.Forward:
                            returnLine = string.Format("return Cursored.EnumerateForward<{0}, {1}>(this.Tokens, \"{2}\", parameters{3}{4});", this.ReturnType, this.CursorElementType, this.Uri, this.JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                            break;
                        case CursorMode.Both:
                            returnLine = string.Format("return Cursored.Enumerate<{0}>(this.Tokens, \"{1}\", mode, parameters{2}{3});", this.CursorElementType, this.Uri, this.JsonPathOrEmpty, CustomBaseUrlOrEmpty);
                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    var signatureBase = "public IEnumerable<" + this.CursorElementType + "> Enumerate" + this.Name + "(";
                    if (this.CursorMode == CursorMode.Both)
                        signatureBase += "EnumerateMode mode, ";
                    foreach (var x in new[]
                    {
                        signatureBase + "params Expression<Func<string, object>>[] parameters)",
                        signatureBase + "IDictionary<string, object> parameters)",
                        signatureBase + "object parameters)"
                    })
                    {
                        l.Add(new Method(x, this.Params, new[] { returnLine }, this.MethodCondition["enumerate"]));
                    }
                }
                dic.Add("enumerate", l.ToArray());
                dic.Add("asyncpe", new[] { this.PEAsync });
                dic.Add("asyncid", new[] { this.IDAsync });
                dic.Add("asynct", new[] { this.TAsync });
                dic.Add("asyncstatic", this.StaticAsync);
                return dic;
            }
        }

        public IEnumerable<Method> Methods
        {
            get
            {
                if (this.OmitExcept.Length == 0)
                    return MethodDic.Select(x => x.Value).SelectMany(x => x);
                else
                    return this.OmitExcept.Where(x => MethodDic.ContainsKey(x)).Select(x => MethodDic[x]).SelectMany(x => x);
            }
        }
    }

    public class Parameter
    {
        public string Kind { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string When { get; set; }
        public bool IsOptional { get { return this.Kind.IndexOf("optional", StringComparison.OrdinalIgnoreCase) != -1; } }
        public string RealName { get { return this.Name.TrimStart('@'); } }
        public string ParameterName { get; set; }

        public Parameter(string kind, string type, string nameToken, string @when)
        {
            var nameTokens = nameToken.Split(new [] { '=' }, 2);

            this.Kind = kind;
            this.Type = type;
            this.Name = nameTokens[0];
            this.When = @when;
            this.ParameterName = nameTokens.Length == 1 ? $"\"{this.Name}\"" : nameTokens[1];
        }
    }

    public class ParameterNameComparer : IEqualityComparer<Parameter>
    {
        public bool Equals(Parameter x, Parameter y)
        {
            if (x == null)
                return y == null;
            if (y == null)
                return false;
            return x.Name == y.Name && x.Type == y.Type;
        }

        public int GetHashCode(Parameter x)
        {
            return x == null ? 0 : (x.Name + x.Type).GetHashCode();
        }
    }

    public class Method
    {
        public string Definition { get; set; }
        public Parameter[] Params { get; set; }
        public string[] Body { get; set; }
        public bool HasStaticArgs { get; set; }
        public bool TakesCancellationToken { get; set; }
        public string WhenClause { get; set; }

        public Method(string definition, IEnumerable<Parameter> parameters, string[] body, string whenClause, bool hasStaticArgs = false, bool takesCancellationToken = false)
        {
            this.Definition = definition;
            this.Params = parameters != null ? parameters.ToArray() : new Parameter[0];
            this.Body = body;
            this.HasStaticArgs = hasStaticArgs;
            this.TakesCancellationToken = takesCancellationToken;

            var staticArgWhen = this.HasStaticArgs
                ? this.Params.Select(p => p.When).SingleOrDefault(x => !string.IsNullOrEmpty(x))
                : null;

            if (!string.IsNullOrEmpty(whenClause))
            {
                if (staticArgWhen != null)
                {
                    this.WhenClause = "(" + whenClause + ") && (" + staticArgWhen + ")";
                }
                else
                {
                    this.WhenClause = whenClause;
                }
            }
            else
            {
                this.WhenClause = staticArgWhen;
            }
        }

        public override string ToString()
        {
            return this.Definition + Environment.NewLine + string.Join(Environment.NewLine, this.Body);
        }
    }

    public class RawLines : ApiEndpoint
    {
        public string[] Lines { get; set; }
    }

    public enum ApiType
    {
        Normal,
        Listed,
        Dictionary,
        Void
    }

    public enum CursorMode
    {
        None,
        Forward,
        Both
    }

    public class Indent
    {
        int indent { get; set; }

        public int Spaces { get; set; }

        public Indent(int i, int s = 4)
        {
            this.indent = i;
            this.Spaces = s;
        }

        public void Inc()
        {
            indent = indent + 1;
        }

        public void Dec()
        {
            indent = indent - 1;
        }

        public override string ToString()
        {
            return new string(' ', Spaces * indent);
        }
    }

    public enum Mode
    {
        none,
        endpoint,
        description,
        returns,
        prms,
        with,
        pe,
        id,
        t,
        stat,
        ape,
        aid,
        at,
        astat,
        jmap
    }

    public class ApiParent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CustomRootNamespace { get; set; }

        public ApiEndpoint[] Endpoints { get; set; }

        public static ApiParent Parse(string fileName, string prefix)
        {
            var ret = new ApiParent();

            var lines = File.ReadAllLines(fileName);
            ret.Name = lines.First(x => x.StartsWith("#namespace")).Split(' ')[1];
            ret.Description = lines.First(x => x.StartsWith("#description")).Replace("#description ", "");
            ret.CustomRootNamespace = lines.Where(x => x.StartsWith("#root")).Select(x => x.Split(' ')[1]).FirstOrDefault();

            const string urlPrefixDirective = "#urlprefix ";
            const string urlSuffixDirective = "#urlsuffix ";
            var urlPrefix = lines.Where(x => x.StartsWith(urlPrefixDirective)).Select(x => x.Substring(urlPrefixDirective.Length)).FirstOrDefault() ?? prefix;
            var urlSuffix = lines.Where(x => x.StartsWith(urlSuffixDirective)).Select(x => x.Substring(urlSuffixDirective.Length)).FirstOrDefault();

            var es = new List<ApiEndpoint>();

            var mode = Mode.none;
            var now = new ApiEndpoint()
            {
                UrlPrefix = urlPrefix,
                UrlSuffix = urlSuffix,
            };
            var s = new List<string>();
            var s2 = new List<string>();
            var commenting = false;
            var cbs = new string[][] { null, null, null, null, null, null, null, null };
            var ats = new List<Tuple<string, string>>();
            var ang = new Dictionary<int, List<Parameter[]>>();

            var jmapi = 0;

            foreach (var i in lines)
            {
                var l = i.TrimStart('\t', ' ');
                if (l.StartsWith("/*") || l.StartsWith("#raw"))
                {
                    commenting = true;
                }
                else if (l.StartsWith("#endraw") || l.StartsWith("*/"))
                {
                    commenting = false;
                    if (l.StartsWith("#endraw"))
                    {
                        es.Add(new RawLines() { Lines = s2.ToArray() });
                    }
                    s2.Clear();
                }
                else if (commenting)
                {
                    s2.Add(i);
                }
                else if (l.StartsWith("endpoint"))
                {
                    var x = l.Split(' ');
                    now.Name = x[2];
                    var rt = x[1];
                    if (rt.StartsWith("void"))
                    {
                        now.ReturnType = "void";
                        now.Type = ApiType.Void;
                    }
                    else if (rt.StartsWith("Listed<"))
                    {
                        now.ReturnType = rt.Remove(rt.LastIndexOf('>')).Substring("Listed<".Length);
                        now.Type = ApiType.Listed;
                    }
                    else if (rt.StartsWith("Dictionary<"))
                    {
                        now.ReturnType = rt.Remove(rt.LastIndexOf('>')).Substring("Dictionary<".Length);
                        now.Type = ApiType.Dictionary;
                    }
                    else
                    {
                        if (rt.StartsWith("Cursored<"))
                        {
                            now.CursorMode = CursorMode.Both;
                            now.CursorElementType = rt.Remove(rt.LastIndexOf('>')).Substring("Cursored<".Length);
                        }

                        now.ReturnType = rt;
                        now.Type = ApiType.Normal;
                    }
                    now.Request = x[4];
                    now.Uri = now.Request == "Impl" ? null : x[5];
                    if (now.Uri?.Any(c => c == '{') ?? false)
                    {
                        var reserveds = new List<string>();
                        var reserved = null as StringBuilder;
                        var inReserved = false;
                        var wontEscape = true;
                        for (var ci = 0; ci < now.Uri.Length; ci++)
                        {
                            var c = now.Uri[ci];
                            switch (c)
                            {
                                case '{':
                                    if (wontEscape)
                                    {
                                        if (inReserved)
                                            throw new FormatException("nested '{' detected");
                                        inReserved = true;
                                        reserved = new StringBuilder(now.Uri.Length - ci - 2);
                                    }
                                    else
                                        goto default;
                                    break;
                                case '}':
                                    if (wontEscape)
                                    {
                                        if (!inReserved)
                                            throw new FormatException("unexpected '}' detected");
                                        inReserved = false;
                                        reserveds.Add(reserved.ToString());
                                    }
                                    else
                                        goto default;
                                    break;
                                case '\\':
                                    if (wontEscape)
                                        wontEscape = false;
                                    else
                                        goto default;
                                    break;
                                default:
                                    if (inReserved)
                                        reserved.Append(c);
                                    wontEscape = true;
                                    break;
                            }
                        }
                        now.ReservedNames = reserveds;
                    }
                    mode = Mode.endpoint;
                }
                else if (l.StartsWith("description"))
                {
                    mode = Mode.description;
                }
                else if (l.StartsWith("returns"))
                {
                    mode = Mode.returns;
                }
                else if (l.StartsWith("params"))
                {
                    mode = Mode.prms;
                }
                else if (l.StartsWith("with"))
                {
                    mode = Mode.with;
                }
                else if (l.StartsWith("pe"))
                {
                    mode = Mode.pe;
                }
                else if (l.StartsWith("id"))
                {
                    mode = Mode.id;
                }
                else if (l.StartsWith("t"))
                {
                    mode = Mode.t;
                }
                else if (l.StartsWith("static"))
                {
                    mode = Mode.stat;
                }
                else if (l.StartsWith("asyncpe"))
                {
                    mode = Mode.ape;
                }
                else if (l.StartsWith("asyncid"))
                {
                    mode = Mode.aid;
                }
                else if (l.StartsWith("asynct"))
                {
                    mode = Mode.at;
                }
                else if (l.StartsWith("asyncstatic"))
                {
                    mode = Mode.astat;
                }
                else if (l.StartsWith("jsonmap"))
                {
                    if (now.Request != "Post" && now.Request != "Put" && now.Request != "Impl")
                        throw new NotSupportedException();
                    mode = Mode.jmap;
                }
                else if (mode == Mode.jmap && l.Contains("{"))
                {
                    jmapi += l.Count(c => c == '{');
                    s.Add(l);
                }
                else if (l.StartsWith("{"))
                {
                }
                else if (l.StartsWith("}"))
                    switch (mode)
                    {
                        case Mode.none:
                            break;
                        case Mode.description:
                            now.Description = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.returns:
                            now.Returns = string.Join(Environment.NewLine, s);
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.prms:
                            now.Params = s.Select(x =>
                                {
                                    var y = x.Split(new [] { ' ' }, 3);
                                    if (y[0].StartsWith("either"))
                                    {
                                        var j = y[0].Split(new[] { '[', ']' });
                                        var index = j.Length > 1 ? int.Parse(j[1]) : 0;
                                        var prms = y.Length == 1 ? new Parameter[0]
                                            : x.Substring(x.IndexOf(' ')).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(z =>
                                            {
                                                var e = z.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                var @when = e.Length >= 4 && e[2] == "when" ? z.Substring(z.LastIndexOf(" when ") + 6) : null;
                                                return z.Length == 0 ? null : new Parameter("any one is required", e[0], e[1], @when);
                                            })
                                            .ToArray();
                                        if (!ang.ContainsKey(index))
                                            ang[index] = new List<Parameter[]>();
                                        ang[index].Add(prms);
                                        return prms;
                                    }
                                    else
                                        return new[] { new Parameter(y[0], y[1], y[2], null) };
                                }).SelectMany(_ => _).Where(x => x != null).ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.with:
                            foreach (var x in s)
                            {
                                if (x.StartsWith("JsonPath="))
                                {
                                    now.JsonPath = x.Substring("JsonPath=".Length);
                                }
                                else if (x.StartsWith("OmitExcept="))
                                {
                                    now.OmitExcept = x.Substring("OmitExcept=".Length).Split(',');
                                }
                                else if (x.StartsWith("["))
                                {
                                    var name = x.Split(new[] { '[', ']' })[1];
                                    ats.Add(Tuple.Create(name, x.Replace("[" + name + "]=", "")));
                                }
                                else if (x.StartsWith("Cursor=Forward<"))
                                {
                                    now.CursorMode = CursorMode.Forward;
                                    now.CursorElementType = x.Remove(x.LastIndexOf('>')).Substring("Cursor=Forward<".Length);
                                }
                                else if (x.StartsWith("When["))
                                {
                                    var match = Regex.Match(x, @"\[([^\]]+)\]=(.*)");
                                    if (!match.Success) throw new FormatException("Invalid When[]");
                                    var key = match.Groups[1].Value;
                                    if (!now.MethodCondition.ContainsKey(key)) throw new FormatException("Invalid key for When[]");
                                    now.MethodCondition[key] = match.Groups[2].Value;
                                }
                                else
                                {
                                    throw new FormatException($"'{x}' is not supported.");
                                }
                            }
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.jmap:
                            jmapi -= l.Count(c => c == '}');
                            s.Add(l);
                            if(jmapi == 0)
                            {
                                // Concatenate lines that do not have '$'
                                for(var index = 1; index < s.Count;)
                                {
                                    if(s[index - 1].IndexOf('$') < 0 && s[index].IndexOf('$') < 0)
                                    {
                                        s[index - 1] += s[index];
                                        s.RemoveAt(index);
                                    }
                                    else
                                    {
                                        index++;
                                    }
                                }

                                now.JsonMap = s.ToArray();
                                s.Clear();
                                mode = Mode.endpoint;
                            }
                            break;
                        case Mode.pe:
                            cbs[0] = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.id:
                            cbs[1] = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.t:
                            cbs[2] = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.stat:
                            cbs[3] = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.ape:
                            cbs[4] = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.aid:
                            cbs[5] = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.at:
                            cbs[6] = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.astat:
                            cbs[7] = s.ToArray();
                            s.Clear();
                            mode = Mode.endpoint; break;
                        case Mode.endpoint:
                            now.CustomBodies = cbs;
                            now.Attributes = ats.ToArray();
                            now.AnyOneGroups = ang.Values.ToList();
                            es.Add(now);

                            now = new ApiEndpoint
                            {
                                UrlPrefix = urlPrefix,
                                UrlSuffix = urlSuffix,
                            };
                            mode = Mode.none;
                            s.Clear();
                            cbs = new string[][] { null, null, null, null, null, null, null, null };
                            ats.Clear();
                            ang.Clear();
                            break;
                    }
                else if (mode == Mode.jmap && l.Contains("}"))
                {
                    jmapi -= l.Count(c => c == '}');
                    s.Add(l);
                }
                else if (!l.StartsWith("#") && !l.StartsWith("//") && !l.All(x => char.IsWhiteSpace(x)) && l != "")
                    s.Add(l);
            }

            ret.Endpoints = es.ToArray();

            return ret;
        }
    }

    public static class Extensions
    {
        internal static string JoinToString<T>(this IEnumerable<T> xs, string sep = null)
        {
            if(sep == null)
                return String.Concat(xs);
            else
                return String.Join(sep, xs);
        }

        static IEnumerable<IEnumerable<T>> Combinate<T>(IEnumerable<IEnumerable<T>> x, IEnumerable<T> y)
        {
            foreach (var a in x)
                foreach (var b in y)
                    yield return a.Concat(new[] { b });
        }

        static IEnumerable<IEnumerable<T>> Flatten<T>(IEnumerable<IEnumerable<T>> x)
        {
            foreach (var i in x)
                foreach (var j in i)
                    yield return new[] { j };
        }

        public static IEnumerable<IEnumerable<T>> Combinate<T>(IEnumerable<IEnumerable<T>> x)
        {
            if (x.Count() < 2)
                return Flatten(x);
            var y = Flatten(x.Take(1));

            foreach (var i in x.Skip(1))
                y = Combinate(y, i);
            return y;
        }
    }
}
