export interface ISpeedStuff {
  href: string;
  id: number;
  name: string;
  public_name?: null;
  organization: OrganizationOrOriginOrDestination;
  origin: OrganizationOrOriginOrDestination;
  destination: OrganizationOrOriginOrDestination;
  enabled: boolean;
  draft: boolean;
  length: number;
  min_number_of_lanes: number;
  minimum_tt: number;
  is_freeway: boolean;
  direction: string;
  coordinates?: null;
  latest_stats: LatestStats;
  latest_source_id_type_stats?: null;
  trend?: null;
  incidents?: (null)[] | null;
  link_params?: null;
  excluded_source_id_types?: null;
  emulated_travel_time?: null;
  closed_or_ignored?: null;
}
export interface OrganizationOrOriginOrDestination {
  href: string;
  id: number;
}
export interface LatestStats {
  interval_start: string;
  travel_time: number;
  delay: number;
  speed: number;
  excess_delay: number;
  congestion: number;
  score: number;
  flow_restriction_score: number;
  average_density: number;
  density: number;
  enough_data: boolean;
  ignored: boolean;
  closed: boolean;
}
