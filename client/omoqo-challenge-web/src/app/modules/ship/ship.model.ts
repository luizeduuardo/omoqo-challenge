export interface ShipListResponse {
  total: number;
  data: ShipList[];
}

export interface ShipList {
  id: number;
  name: string;
  length: number;
  width: number;
}

export interface Ship {
  id: number;
  name: string;
  length: number;
  width: number;
}
