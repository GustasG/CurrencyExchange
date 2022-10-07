import { toast } from "react-toastify";
import DatePicker from "react-datepicker";
import { useState, useEffect } from "react";
import { Row, Col, InputGroup } from "react-bootstrap";

import DropDown from "../components/DropDown";

import { dateToIsoDateOnlyString } from "../util";

import { useAppSelector, useAppDispatch } from "../hooks/typedActions";

import NumberInput from "../components/NumberInput";
import { useGetDatesQuery, useGetCurrenciesMutation, useGetExchangeRateMutation } from "../redux/api/historicalExchangeApi";
import { setDate, setCurrencyFrom, setCurrencyTo, setAmount, setRate } from "../redux/slices/historicalExcangeSlice";

export default function HistoricalExchange() {
  const { data: dates, isLoading, error } = useGetDatesQuery();
  const [updateCurrencies] = useGetCurrenciesMutation();
  const [updateRate] = useGetExchangeRateMutation();

  const dispatch = useAppDispatch();
  const date = useAppSelector(state => state.historicalExchange.date);
  const currencyFrom = useAppSelector(state => state.historicalExchange.currencyFrom);
  const currencyTo = useAppSelector(state => state.historicalExchange.currencyTo);
  const amount = useAppSelector(state => state.historicalExchange.amount);
  const rate = useAppSelector(state => state.historicalExchange.rate);

  const [displayedDate, setDisplayedDate] = useState(new Date(date));
  const [currencies, setCurrencies] = useState<string[]>([]);
  const [dateLookupSet, setDateLookupSet] = useState(new Set<string>());

  const currentDateIsValid = dateLookupSet.has(date);

  useEffect(() => {
    setDateLookupSet(new Set<string>(dates));

    if (dates) {
      const updatedDate = dates[dates.length - 1];

      dispatch(setDate(updatedDate));
      setDisplayedDate(new Date(updatedDate));
    }
  }, [dates, setDateLookupSet, dispatch]);

  useEffect(() => {
    if (!currentDateIsValid) {
      return;
    }

    updateCurrencies({
      date: date
    })
    .unwrap()
    .then(data => {
      setCurrencies(data);
    })
    .catch(() => {
      toast.error("Failed to fetch available currencies for given date");
    });
  }, [date, currentDateIsValid, updateCurrencies, dispatch]);

  const isValidCurrencyFrom = currencies.includes(currencyFrom);
  const isValidCurrencyTo = currencies.includes(currencyTo);

  useEffect(() => {
    if (!isValidCurrencyFrom) {
      dispatch(setCurrencyFrom(currencies[0]));
    }
    if (!isValidCurrencyTo) {
      dispatch(setCurrencyTo(currencies[0]));
    }
  }, [currencies, isValidCurrencyFrom, isValidCurrencyTo, dispatch]);

  useEffect(() => {
    if (!currentDateIsValid || currencyFrom === "" || currencyTo === "") {
      return;
    }

    updateRate({
      date: date,
      from: currencyFrom,
      to: currencyTo
    })
    .unwrap()
    .then(data => dispatch(setRate(data)))
    .catch(() => toast.error("Failed to fetch exchange rate"));
  }, [currentDateIsValid, date, currencyFrom, currencyTo, updateRate, dispatch]);

  const handleDateChange = (date: Date | null) => {
    if (date && dateLookupSet.has(dateToIsoDateOnlyString(date))) {
      setDisplayedDate(date);
      dispatch(setDate(dateToIsoDateOnlyString(date)));
    }
  };

  if (isLoading) {
    return <p>Loading...</p>
  }

  if (error || !dates) {
    return <p>Failed to fetch available dates</p>
  }

  const exchangedAmount = rate * amount;

  return (
    <>
      <h1>Convert currency at date</h1>
      <hr />
      <Row className="pt-4">
        <Col md={4}>
          <InputGroup className="mb-3">
            <InputGroup.Text>Date</InputGroup.Text>
            <DatePicker
              className="form-control"
              dateFormat="yyyy-MM-dd"
              calendarStartDay={1}
              selected={displayedDate}
              filterDate={(testDate) => dateLookupSet.has(dateToIsoDateOnlyString(testDate))}
              onChange={handleDateChange} />
          </InputGroup>
          <DropDown label="From" options={currencies} value={currencyFrom} onChange={(value) => dispatch(setCurrencyFrom(value))} />
          <DropDown label="To" options={currencies} value={currencyTo} onChange={(value) => dispatch(setCurrencyTo(value))} />
          <NumberInput label="Amount" initialValue={amount} onChange={(value) => dispatch(setAmount(value))} />
        </Col>
        <Col md={{span: 4, offset: 2}}>
          <p>Date: {date}</p>
          <p>{currencyFrom}/{currencyTo} Exchange Rate: <span>{rate.toFixed(2)}</span></p>
          <p>Converted Amount: <span>{exchangedAmount.toFixed(2)}</span></p>
        </Col>
      </Row>
    </>
  )
}