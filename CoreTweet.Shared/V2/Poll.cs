// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2018 CoreTweet Development Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Runtime.Serialization;
using System.Text;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoreTweet.V2
{
    public class Poll : CoreBase
    {
        /// <summary>
        /// Unique identifiers of the expanded poll.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Contains objects describing each choice in the referenced poll.
        /// </summary>
        [JsonProperty("options")]
        public PollOption[] Options { get; set; }

        /// <summary>
        /// Indicates if this poll is still active and can receive votes, or if the voting is now closed.
        /// </summary>
        [JsonProperty("voting_status")]
        public PollVotingStatus VotingStatus { get; set; }
        // MEMO: the document is wrong (actual property position is `polls.voting_status`, not `polls.options.voting_status`)

        /// <summary>
        /// Specifies the end date and time for this poll.
        /// </summary>
        [JsonProperty("end_datetime")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset EndDatetime { get; set; }
        // MEMO: the document is wrong (actual value type is ISO 8601 formatted `string`, not `number`)

        /// <summary>
        /// Specifies the total duration of this poll.
        /// </summary>
        [JsonProperty("duration_minutes")]
        public int DurationMinutes { get; set; }
    }

    public class PollOption
    {
        /// <summary>
        /// Position of this choice in the poll.
        /// </summary>
        [JsonProperty("position")]
        public int Position { get; set; }

        /// <summary>
        /// Text of the poll choice.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Number of votes this poll choice received.
        /// </summary>
        [JsonProperty("votes")]
        public int Votes { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PollVotingStatus
    {
        [EnumMember(Value = "open")]
        Open,
        [EnumMember(Value = "closed")]
        Closed,
    }

    /// <summary>
    /// List of fields to return in the Tweet poll object. The response will contain the selected fields only if a Tweet contains a poll.
    /// </summary>
    [Flags]
    public enum PollFields
    {
        None            = 0x00000000,
        DurationMinutes = 0x00000001,
        EndDatetime     = 0x00000002,
        Id              = 0x00000004,
        Options         = 0x00000008,
        VotingStatus    = 0x00000010,
        All             = 0x0000001f,
    }

    public static class PollFieldsExtensions
    {
        public static string ToQueryString(this PollFields value)
        {
            if (value == PollFields.None)
                return "";

            var builder = new StringBuilder();

            if ((value & PollFields.DurationMinutes) != 0)
                builder.Append("duration_minutes,");
            if ((value & PollFields.EndDatetime) != 0)
                builder.Append("end_datetime,");
            if ((value & PollFields.Id) != 0)
                builder.Append("id,");
            if ((value & PollFields.Options) != 0)
                builder.Append("options,");
            if ((value & PollFields.VotingStatus) != 0)
                builder.Append("voting_status,");

            return builder.ToString(0, builder.Length - 1);
        }
    }
}
