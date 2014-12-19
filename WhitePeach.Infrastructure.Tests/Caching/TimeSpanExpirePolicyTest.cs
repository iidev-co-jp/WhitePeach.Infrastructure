using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhitePeach.Infrastructure.Caching;
using Xunit;

namespace WhitePeach.Infrastructure.Tests.Caching
{
    public class TimeSpanExpirePolicyTest
    {
        [Fact]
        public void Expire()
        {
            var policy = new TimeSpanExpirePolicy(new TimeSpan(0, 0, 0, 0, -1));
            Assert.False(policy.Validate(new CacheEntry<int> { UpdateTime = DateTime.Now })); // DateTime.Now が 0001 年 1 月 1 日の 00:00:00.001 以前だと失敗する可能性あり
        }

        [Fact]
        public void NotExpire()
        {
            var policy = new TimeSpanExpirePolicy(new TimeSpan(1, 0, 0));
            Assert.True(policy.Validate(new CacheEntry<int> { UpdateTime = DateTime.Now.AddSeconds(-1) })); //中で呼び出されるDateTime.Nowより必ず小さくしている
        }
    }
}
