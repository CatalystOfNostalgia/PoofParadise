import time

# We use unix time for all server time calculations.
class ServerTime():
    SECONDS_PER_HOUR = 3600.0

    def _get_server_time(self):
        return int(time.time())

    def _get_time_passed(self, old_time):
        # Returns the time passed in  seconds. Resource production rate is a function in hours.
        return int(self._get_server_time() - old_time)

    def calculate_resource_gain(self, old_time, resource_production_rate):
        time_passed_in_hours = self.get_time_passed(old_time)/SECONDS_PER_HOUR
        resources_gained = resource_production_rate * time_passed_in_hours
        if resources_gained < 0:
            return 0
        return resources_gained
