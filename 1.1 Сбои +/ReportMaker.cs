using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{
    public class Failure
    {
        public int FailureType { get; set; }
        public int DeviceId { get; set; }
        public DateTime Time { get; set; }
    }
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
    }
    public enum FailureType
    {
        UnexpectedShutdown = 0,
        ShortNonResponding = 1,
        HardwareFailure = 2,
        ConnectionProblem = 3
    }
    public class ReportMaker
    {
        public static List<string> FindDevicesFailedBeforeDate(
            DateTime targetDate,
            IEnumerable<Failure> failures,
            IEnumerable<Device> devices)
        {
            var problematicDevices = failures
                .Where(failure => IsFailureSerious(failure.FailureType) && failure.Time < targetDate)
                .Select(failure => failure.DeviceId)
                .Distinct();

            var result = devices
                .Where(device => problematicDevices.Contains(device.DeviceId))
                .Select(device => device.Name)
                .ToList();

            return result;
        }

        private static bool IsFailureSerious(int failureType)
        {
            return failureType % 2 == 0;
        }

        public static List<string> FindDevicesFailedBeforeDateObsolete(
        int day,
        int month,
        int year,
        int[] failureTypes,
        int[] deviceId,
        object[][] times,
        List<Dictionary<string, object>> devices)
        {
            var failures = Enumerable.Range(0, failureTypes.Length)
                .Select(i => new Failure
                {
                    FailureType = failureTypes[i],
                    DeviceId = deviceId[i],
                    Time = new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0])
                });

            var deviceList = devices.Select(d => new Device
            {
                DeviceId = (int)d["DeviceId"],
                Name = d["Name"] as string
            });

            var targetDate = new DateTime(year, month, day);

            return FindDevicesFailedBeforeDate(targetDate, failures, deviceList);
        }
    }
}
