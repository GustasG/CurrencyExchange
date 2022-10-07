export interface HistoricalExchangeCurrencyRequest {
  date: string
};

export interface HistoricalExchangeRateRequest {
  date: string,
  from: string,
  to: string
}